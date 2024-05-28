using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Postgrest;
using SmartTrade.Entities;
using SmartTrade.Persistencia.DataAccess;
using SmartTrade.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartTrade.Logica.Services
{
    public class STService : ISTService
    {
        private readonly IDAL dal;
        private static STService instance;
        private Usuario loggedUser;

        private STService(IDAL dal)
        {
            this.dal = dal;
        }

        public static STService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new STService(new STDAL(new SupabaseContext()));
                }
                return instance;
            }
        }

        public async Task AddUser(Usuario usuario)
        {
            if (IsEmailAlreadyRegistered(usuario.Email))
            {
                throw new EmailYaRegistradoException();
            }
            else if (await IsNicknameAlreadyRegistered(usuario.Nickname))
            {
                throw new NickYaRegistradoException();
            }
            else
            {
                if (IsValidEmailFormat(usuario.Email))
                {
                    await dal.Add<Usuario>(usuario);
                }
                else
                {
                    throw new EmailFormatoIncorrectoException();
                }
            }
        }

        private bool IsEmailAlreadyRegistered(string email)
        {
            Usuario usuario = dal.GetByEmail(email);
            return usuario != null;
        }

        private async Task<bool> IsNicknameAlreadyRegistered(string nickname)
        {
            return await dal.GetById<Usuario>(nickname) != null;
        }

        private bool IsValidEmailFormat(string email)
        {
            return email.Contains("@") && (email.EndsWith(".com") || email.EndsWith(".es"));
        }

        public bool MayorDe18(DateTime fecha_nac)
        {
            TimeSpan edad = DateTime.Now - fecha_nac;
            return (int)(edad.TotalDays / 365.25) >= 18;
        }

        public async Task AddProducto(Producto producto)
        {
            await dal.Add<Producto>(producto);
            dal.Commit();
        }

        public async Task AddProductoVendedor(Producto_vendedor producto_vendedor)
        {
            await dal.Add<Producto_vendedor>(producto_vendedor);
        }

        public async Task<bool> Login(string identifier, string password)
        {
            try
            {
                Usuario usuario = await dal.GetById<Usuario>(identifier);
                if (usuario == null)
                {
                    usuario = dal.GetByEmail(identifier);
                    if (usuario == null)
                    {
                        return false; // El usuario no está registrado ni por nickname ni por correo
                    }
                }

                if (usuario.Password != password)
                {
                    return false; // La contraseña no coincide
                }

                // Inicio de sesión exitoso
                loggedUser = usuario;
                return true;
            }
            catch (Exception)
            {
                throw new ServiceException("Ha ocurrido un error al iniciar sesión. Por favor, inténtelo más tarde");
            }
        }

        public async Task<List<Producto>> GetProductosPorCategoria(string categoria)
        {
            var productos = await dal.GetAll<Producto>();
            var productosPorCategoria = productos.Where(p => p.Categoria == categoria);
            return productosPorCategoria.ToList();
        }

        public async Task<List<Producto>> GetAllProductos()
        {
            try
            {
                List<Producto> productos = await GetProductosPorCategoria("Deporte");
                productos.AddRange(await GetProductosPorCategoria("Ropa"));
                productos.AddRange(await GetProductosPorCategoria("Papeleria"));
                productos.AddRange(await GetProductosPorCategoria("Tecnologia"));

                List<Producto_vendedor> productoVendedor = await dal.GetAll<Producto_vendedor>();
                productos.ForEach(p => p.Producto_Vendedor = productoVendedor.Where(pv => pv.IdProducto == p.Id).ToList());

                if (loggedUser != null)
                {
                    await ProcesarListaDeDeseosYAlertas(productos);
                }

                return productos;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener los productos", e);
            }
        }

        private async Task ProcesarListaDeDeseosYAlertas(List<Producto> p)
        {
            List<ListaDeseosItem> listaDeseos = await GetListaDeseos(GetLoggedNickname());

            foreach (ListaDeseosItem item in listaDeseos)
            {
                await AgregarProductoAListaDeDeseos(item);
                await NotificarSiStockEsCero(item);
            }
        }

        private async Task AgregarProductoAListaDeDeseos(ListaDeseosItem item)
        {
            Producto_vendedor pv = await dal.GetById<Producto_vendedor>(item.ProductoVendedorId);
            Producto p = await dal.GetById<Producto>(pv.IdProducto);

            if (p != null)
            {
                p.AddObservador(loggedUser);
            }
        }

        private async Task NotificarSiStockEsCero(ListaDeseosItem item)
        {
            Producto_vendedor pv = await dal.GetById<Producto_vendedor>(item.ProductoVendedorId);
            Producto p = await dal.GetById<Producto>(pv.IdProducto);

            if (p != null && pv.Stock == 0)
            {
                p.NotificarObservadores();
            }
        }

        public async Task<List<ListaDeseosItem>> GetListaDeseos(string nickPropietario)
        {
            try
            {
                var listaDeseos = await dal.GetAll<ListaDeseosItem>();
                return listaDeseos.Where(ld => ld.NickPropietario == nickPropietario).ToList();
            } catch (Exception e)
            {
                throw new ServiceException("Error al obtener la lista de deseos", e);
            }

        }

        public async Task<Producto> GetProductoById(int id)
        {
            try
            {
                return await dal.GetById<Producto>(id);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener el producto", e);
            }
        }

        public async Task<Producto> GetProductoByIdProductoVendedor(int idProductoVendedor)
        {
            try
            {
                List<Producto_vendedor> productoVendedorList = await dal.GetAll<Producto_vendedor>();
                Producto_vendedor pv = productoVendedorList.Where(aux => aux.Id == idProductoVendedor).FirstOrDefault();

                if (pv == null)
                {
                    throw new Exception("No se ha encontrado el producto asociado al vendedor");
                }
                else
                {
                    return await GetProductoById(pv.IdProducto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto: {ex.Message}");
                throw new Exception("Error al obtener el producto", ex);
            }
        }

        public async Task <Producto_vendedor> GetProductoVendedorById(int id)
        {
            try
            {
                return await dal.GetById<Producto_vendedor>(id);
            } catch (Exception e)
            {
                throw new ServiceException("Error al obtener la asociacion entre el producto y el vendedor", e);
            }
        }

        public async Task<List<ItemCarrito>> GetCarrito()
        {
            try
            {
                List<ItemCarrito> productosCarrito = await dal.GetAll<ItemCarrito>();
                return productosCarrito.Where(p => p.NicknameUsuario == loggedUser.Nickname).ToList();
            } catch (Exception e)
            {
                throw new ServiceException("Error al obtener el carrito", e);
            }

        }

        public string GetLoggedNickname()
        {
            return loggedUser.Nickname;
        }

        public bool IsVendedor()
        {
            return loggedUser.IsVendedor;
        }

        public async Task<List<Producto>> GetProductosDeVendedor(string nickname)
        {
            try
            {
                List<Producto_vendedor> productosVendedor = await dal.GetAll<Producto_vendedor>();
                productosVendedor = productosVendedor.Where(pv => pv.NicknameVendedor == nickname).ToList();

                List<Producto> productos = await dal.GetAll<Producto>();
                productos = productos.Where(p => productosVendedor.Select(pv => pv.IdProducto).Contains(p.Id)).ToList();
                productos.ForEach(p => p.Producto_Vendedor = productosVendedor.Where(pv => pv.IdProducto == p.Id).ToList());

                return productos;
            } catch (Exception e)
            {
                throw new ServiceException("Error al obtener los productos del vendedor '" + nickname + "'", e);
            }

        }

        public async Task<bool> ActualizarItemCarrito(ItemCarrito item)
        {
            try
            {
                await dal.Update<ItemCarrito>(item);
                return true;
            } catch (Exception e)
            {
                throw new ServiceException("Error al actualizar el item del carrito. Vuelva a intentarlo más tarde", e);
            }
        }

        public async Task<bool> EliminarItemCarrito(ItemCarrito item)
        {
            try
            {
                await dal.Delete<ItemCarrito>(item);
                return true;
            } catch (Exception e)
            {
                throw new ServiceException("Error al eliminar el item del carrito. Vuelva a intentarlo más tarde", e);
            }
        }

        public async Task<bool> AgregarItemCarrito(ItemCarrito item)
        {
            try
            {
                //get if the item is already in the cart
                List<ItemCarrito> items = await GetCarrito();
                ItemCarrito itemCarrito = items.Where(i => i.idProductoVendedor == item.idProductoVendedor && item.Caracteristica == i.Caracteristica).FirstOrDefault();
                if (itemCarrito != null)
                {
                    itemCarrito.Cantidad += item.Cantidad;
                    await dal.Update<ItemCarrito>(itemCarrito);
                    return true;
                }
                else
                {
                    await dal.Add<ItemCarrito>(item);
                    return true;
                }
            } catch (Exception e)
            {
                throw new ServiceException("Error al añadir el item al carrito. Vuelva a intentarlo más tarde", e);
            }
        }

        public async Task<List<Producto_vendedor>> GetAProductoVendedorByProducto(Producto p) 
        {
           try
           {
                List<Producto_vendedor> pvList = await dal.GetAll<Producto_vendedor>();
                return pvList.Where(pv => pv.IdProducto == p.Id).ToList();
           } catch (Exception e)
            {
                throw new ServiceException("Error al obtener los productos vendedor asociados al producto", e);
           }
        }

        public async Task<List<Producto>> getProductosListaDeseos()
        {
            try
            {
                List<ListaDeseosItem> listaDeseos = await GetListaDeseos(GetLoggedNickname());
                List<Producto> productos = new List<Producto>();
                foreach (ListaDeseosItem item in listaDeseos)
                {
                    Producto_vendedor pv = await dal.GetById<Producto_vendedor>(item.ProductoVendedorId);
                    Producto p = await dal.GetById<Producto>(pv.IdProducto);
                    p.Producto_Vendedor = new List<Producto_vendedor> { pv };
                    productos.Add(p);
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error al obtener los productos de la Lista de Deseos", ex);
            }
        }

        public async Task EliminarProductoListaDeseos(Producto_vendedor pv)
        {
            try {
                List<ListaDeseosItem> ListaLoggedUser = await GetListaDeseos(GetLoggedNickname());
                foreach (ListaDeseosItem item in ListaLoggedUser)
                {
                    Producto p = await dal.GetById<Producto>(pv.IdProducto);
                    if (item.ProductoVendedorId == pv.Id)
                    {
                        await dal.Delete<ListaDeseosItem>(item);
                        loggedUser.AlertasProductosSinStock.Remove(p);
                        p.RemoveObservador(loggedUser);
                    }
                }
            }catch (Exception ex)
            {
                throw new ServiceException("Error al eliminar el producto de la Lista de Deseos", ex);
            }
            
        }

        public async Task AgregarProductoListaDeseos(Producto_vendedor pv)
        {
            try
            {
                string propietario = GetLoggedNickname();
                ListaDeseosItem ld = new ListaDeseosItem(propietario, pv.Id);
                bool estaEnLista = await ProductoEnListaDeseos(loggedUser, pv);
                if (!estaEnLista)
                {
                    Producto producto = await GetProductoByIdProductoVendedor(pv.Id);
                    await dal.Add<ListaDeseosItem>(ld);
                    producto.AddObservador(loggedUser);
                    if (pv.Stock == 0) producto.NotificarObservadores();
                }
            } catch (Exception ex)
            {
                throw new ServiceException("Error al añadir el producto a la Lista de Deseos", ex);
            }

        }

        public async Task<Boolean> ProductoEnListaDeseos(Usuario user, Producto_vendedor pv)
        {
            try
            {
                List<ListaDeseosItem> listaDeseos = await GetListaDeseos(user.Nickname);
                ListaDeseosItem item = listaDeseos.Where(i => i.ProductoVendedorId == pv.Id).FirstOrDefault();
                return item != null;
            } catch (Exception ex)
            {
                throw new ServiceException("Error al comprobar si el producto está en la Lista de Deseos", ex);
            }
        }

        public async Task<List<Producto>> GetProductosGuardarMasTarde()
        {
            try
            {
                string nickPropietario = GetLoggedNickname();
                List<int> productoslistaMasTarde = new List<int>();
                List<Producto> listaMasTarde = new List<Producto>();
                productoslistaMasTarde = await GetIdProductosGuardarMasTarde();

                if (productoslistaMasTarde != null)
                {
                    Producto p = null;

                    foreach (int idproducto in productoslistaMasTarde)
                    {
                        p = await dal.GetById<Producto>(idproducto);
                        if (p != null)
                        {
                            listaMasTarde.Add(p);
                        }
                    }
                }
                return listaMasTarde;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los productos de la Lista de Deseos. " + ex.Message);
                return null;
            }
        }

        public async Task EliminarProductoGuardarMasTarde(Producto productoLista)
        {
            try
            {
                List<GuardarMasTardeItem> ListaLoggedUser = await GetGuardarMasTarde();
                foreach (var item in ListaLoggedUser)
                {
                    if (item.ProductoId == productoLista.Id)
                    {
                        await dal.Delete<GuardarMasTardeItem>(item);
                        productoLista.RemoveObservador(loggedUser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error al eliminar el producto de la Lista de Deseos", ex);
            }

        }

        public async Task AgregarProductoGuardarMasTarde(Producto producto)
        {
            try
            {
                string propietario = GetLoggedNickname();
                GuardarMasTardeItem gmt = new GuardarMasTardeItem(propietario, producto.Id);
                bool estaEnLista = await ProductoEnGuardarMasTarde(producto);

                if (!estaEnLista)
                {
                    await dal.Add<GuardarMasTardeItem>(gmt);
                    producto.AddObservador(loggedUser);
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error al añadir el producto a la Lista de Deseos", ex);
            }

        }

        public async Task<Boolean> ProductoEnGuardarMasTarde(Producto producto)
        {
            try
            {
                string propietario = GetLoggedNickname();
                List<GuardarMasTardeItem> gmt = await GetGuardarMasTarde();
                GuardarMasTardeItem item = gmt.Where(ld => ld.ProductoId == producto.Id).FirstOrDefault();
                return item != null;
            } catch (Exception ex)
            {
                throw new ServiceException("Error al comprobar si el producto está en la Lista de Deseos", ex);
            }
        }

        public async Task<List<GuardarMasTardeItem>> GetGuardarMasTarde()
        {
            try
            {
                List<GuardarMasTardeItem> productosGuardarMasTarde = await dal.GetAll<GuardarMasTardeItem>();
                return productosGuardarMasTarde.Where(p => p.NickPropietario == loggedUser.Nickname).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener la lista de deseos", e);
            }
        }

        public async Task<List<int>> GetIdProductosGuardarMasTarde()
        {
            try
            {
                List<GuardarMasTardeItem> listaGuardarMasTarde = await GetGuardarMasTarde();
                return listaGuardarMasTarde.Select(ld => ld.ProductoId).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener los productos de la lista de deseos", e);
            }
        }

        public Usuario GetUsuarioLogueado()
        {
            return loggedUser;
        }

        public async Task<List<Pedido>> GetPedidos()
        {
            try
            {
                List<Pedido> pedidos = await dal.GetAll<Pedido>();
                return pedidos.Where(p => p.NickComprador == loggedUser.Nickname).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener los pedidos", e);
            }
        }

        public async Task CargarDetallesPedido(Pedido pedido)
        {
            try
            {
                List<ItemCarrito> itemsPedido = await dal.GetAll<ItemCarrito>();
                //guardar en una List<ItemCarrito> los productos del pedido
                //List<ItemCarrito> itemsPedido = pedido.ItemsCarrito.Select(i => dal.GetById<ItemCarrito>(i).Result).ToList();
                pedido.IdProductoVendedor = itemsPedido.Where(i => i.Id == pedido.Id).Select(i => i.idProductoVendedor).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al cargar los detalles del pedido", e);
            }
        }

        //metodo para obtener la imagen de a partir de un objeto de tipo Pedido, teniendo los id de la lista de productos
        public ImageSource GetImagenPedido(Pedido pedido)
        {
            try
            {
                List<ItemCarrito> itemsPedido = dal.GetAll<ItemCarrito>().Result;
                List<Producto_vendedor> productosVendedor = dal.GetAll<Producto_vendedor>().Result;
                List<Producto> productos = dal.GetAll<Producto>().Result;
                List<Producto> productosPedido = new List<Producto>();

                foreach (int id in pedido.IdProductoVendedor)
                {
                    Producto_vendedor pv = productosVendedor.Where(pvTemp => pvTemp.Id == id).FirstOrDefault();
                    Producto p = productos.Where(prod => prod.Id == pv.IdProducto).FirstOrDefault();
                    productosPedido.Add(p);
                }

                return ImageSource.FromUri(new Uri(productosPedido[0].Imagen));
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener la imagen del pedido", e);
            }
        }

        //metodo para obtener el atributo nombre a partir de un entero (id del producto de la lista de productos de un objeto de tipo pedido)
        public string GetNombreProductoPedido(int idProducto)
        {
            try
            {
                List<Producto_vendedor> productosVendedor = dal.GetAll<Producto_vendedor>().Result;
                List<Producto> productos = dal.GetAll<Producto>().Result;
                Producto_vendedor pv = productosVendedor.Where(pvTemp => pvTemp.Id == idProducto).FirstOrDefault();
                Producto p = productos.Where(prod => prod.Id == pv.IdProducto).FirstOrDefault();
                return p.Nombre;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener el nombre del producto del pedido", e);
            }
        }

        //metodo para obtener el atributo descripción a partir de un entero (id del producto de la lista de productos de un objeto de tipo pedido)
        public string GetDescripcionProductoPedido(int idProducto)
        {
            try
            {
                List<Producto_vendedor> productosVendedor = dal.GetAll<Producto_vendedor>().Result;
                List<Producto> productos = dal.GetAll<Producto>().Result;
                Producto_vendedor pv = productosVendedor.Where(pvTemp => pvTemp.Id == idProducto).FirstOrDefault();
                Producto p = productos.Where(prod => prod.Id == pv.IdProducto).FirstOrDefault();
                return p.Descripcion;
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al obtener la descripción del producto del pedido", e);
            }
        }

        //metodo para para establecer el estado 
        public void ActualizarEstadoPedido(Pedido pedido)
        {
            try
            {
                pedido.Estado = "En preparación";
                DateTime fechaActual = DateTime.Now;
                DateTime fechaPedido = pedido.Fecha;
                TimeSpan diferencia = fechaActual - fechaPedido;
                if (diferencia.Days == 1)
                {
                    pedido.Estado = "Enviado";
                    dal.Update<Pedido>(pedido);
                }
                else if (diferencia.Days == 5)
                {
                    pedido.Estado = "Entregado";
                    dal.Update<Pedido>(pedido);
                }
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al actualizar el estado del pedido", e);
            }
        }
        
        //metodo para cancelar un pedido
        public async Task CancelarPedido(Pedido pedido)
        {
            try
            {
                await dal.Delete<Pedido>(pedido);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al cancelar el pedido", e);
            }
        }

        //metodo para devolver un pedido
        public async Task DevolverPedido(Pedido pedido)
        {
            try
            {
                await dal.Update<Pedido>(pedido);
            }
            catch (Exception e)
            {
                throw new ServiceException("Error al devolver el pedido", e);
            }
        }

        //metodo para obtener la lista de productos de un pedido
        public async Task<List<Producto>> GetProductosPedido(Pedido pedido)
        {
            try
            {
                List<Producto> productosPedido = new List<Producto>();

                foreach (int id in pedido.IdProductoVendedor)
                {
                    Console.WriteLine("[+] Id: " + id);
                    Producto_vendedor pv = await dal.GetById<Producto_vendedor>(id);
                    Console.WriteLine("[+] Producto vendedor encontrado: " + pv.Id);
                    Producto p = await dal.GetById<Producto>(pv.IdProducto);
                    p.Producto_Vendedor.Add(pv);
                    productosPedido.Add(p);
                    Console.WriteLine("[*] Producto: " + p.Nombre);
                }
                return productosPedido;

            }
            catch (Exception e)
            { 
                throw new ServiceException("Error al obtener los productos del pedido", e);
            }
        }

        public async Task<List<Tarjeta>> getTarjetas() 
        {
            try
            {
               
                Usuario user = GetUsuarioLogueado();
                if (loggedUser == null)
                {
                    Console.WriteLine("Error: Usuario no está logueado.");
                    return null;
                }
                var tarjetas = await dal.GetAll<Tarjeta>();
                if (tarjetas == null)
                {
                    Console.WriteLine("Error: No se pudieron obtener las tarjetas.");
                    return null;
                }

                return tarjetas.Where(tarj => tarj.Nick_comprador == loggedUser.Nickname).ToList();

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener tarjetas: ", e.Message);
                return null;
            }

        }
        public async Task <Tarjeta> getTarjetaById(string idTarjeta)
        {
            return await dal.GetById<Tarjeta>(idTarjeta);
        }

    }
}
