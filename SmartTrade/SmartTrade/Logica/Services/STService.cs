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
            if (dal.GetByEmail(usuario.Email) != null)
            {
                throw new EmailYaRegistradoException();
            }
            else if (await dal.GetById<Usuario>(usuario.Nickname) != null)
            {
                throw new NickYaRegistradoException();
            }
            else
            {
                if (usuario.Email.Contains("@"))
                {
                    if (usuario.Email.StartsWith("@"))
                        throw new EmailFormatoIncorrectoException();
                    if (usuario.Email.EndsWith(".com") || usuario.Email.EndsWith(".es"))
                    {
                        await dal.Add<Usuario>(usuario);
                    }
                    else throw new EmailFormatoIncorrectoException();
                }
                else throw new EmailFormatoIncorrectoException();
            }
        }

        public bool MayorDe18(DateTime fecha_nac)
        {
            TimeSpan edad = DateTime.Now - fecha_nac;
            if ((int)(edad.TotalDays / 365.25) >= 18) return true;
            else return false;
        }

        public async Task AddUserVendedor(Vendedor vendedor)
        {

            await dal.Add<Vendedor>(vendedor);
        }

        public async Task AddUserComprador(Comprador comprador)
        {
            await dal.Add<Comprador>(comprador);
        }

        public async Task AddTarjeta(Tarjeta tarjeta)
        {
            await dal.Add<Tarjeta>(tarjeta);
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
                // Manejar la excepción adecuadamente, por ejemplo, registrándola o notificando al usuario
                return false;
            }
        }

        // metodo para ordenar los productos por categorías
        public async Task<List<Producto>> GetProductosPorCategoria(string categoria)
        {
            var productos = await dal.GetAll<Producto>();
            var productosPorCategoria = productos.Where(p => p.Categoria == categoria);
            return productosPorCategoria.ToList();
        }

        public async Task<List<Producto>> GetAllProductos()
        {
            try {
                List<Producto> productos = await GetProductosPorCategoria("Deporte");
                productos.AddRange(await GetProductosPorCategoria("Ropa"));
                productos.AddRange(await GetProductosPorCategoria("Papeleria"));
                productos.AddRange(await GetProductosPorCategoria("Tecnologia"));

                List<Producto_vendedor> productoVendedor = await dal.GetAll<Producto_vendedor>();
                productos.ForEach(p => p.Producto_Vendedor = productoVendedor.Where(pv => pv.IdProducto == p.Id).ToList());

                //agregar observadores a los productos
                List<ListaDeseosItem> listaDeseos = await dal.GetListaDeseos(GetLoggedNickname());
                foreach (ListaDeseosItem item in listaDeseos)
                {
                    Producto_vendedor pv = await dal.GetById<Producto_vendedor>(item.ProductoVendedorId);
                    Producto p = await dal.GetById<Producto>(pv.IdProducto);

                    if (p != null)
                    {
                        p.AddObservador(loggedUser);
                    }
                }

                //notificar a los observadores si el stock es 0
                foreach (ListaDeseosItem item in listaDeseos)
                {
                    Producto_vendedor pv = await dal.GetById<Producto_vendedor>(item.ProductoVendedorId);
                    Producto p = await dal.GetById<Producto>(pv.IdProducto);
                    if (p != null)
                    {
                        if (pv.Stock == 0)
                        {
                            p.NotificarObservadores();
                        }
                    }
                }
                return productos;
            }
            catch (Exception e) { 
                Console.WriteLine("Error al obtener los productos: ", e.Message);
                return null; 
            }
        }
        public async Task<List<ListaDeseosItem>> GetListaDeseos(string nickPropietario)
        {
            var listaDeseos = await dal.GetAll<ListaDeseosItem>();
            return  listaDeseos.Where(ld => ld.NickPropietario == nickPropietario).ToList();

        }
        public Producto GetProductoById(string id)
        {
            try
            {
                return dal.GetById<Producto>(id).Result;
            } catch (Exception e)
            {
                Console.WriteLine("Error al obtener el producto: ", e.Message);
                return null;
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
                Console.WriteLine("Error al obtener el producto: ", e.Message);
                return null;
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
                    Console.WriteLine("Producto vendedor no encontrado");
                    return null;
                }
                else
                {
                    //No funciona el getById del DAL
                    List<Producto> productos = await dal.GetAll<Producto>();
                    return productos.Where(p => p.Id == pv.IdProducto).FirstOrDefault();
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
                Console.WriteLine("Error al obtener el producto vendedor: ", e.Message);
                return null;
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
                Console.WriteLine("Error al obtener el carrito: ", e.Message);
                return null;
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
            List<Producto_vendedor> productosVendedor = await dal.GetAll<Producto_vendedor>();
            productosVendedor = productosVendedor.Where(pv => pv.NicknameVendedor == nickname).ToList();

            List<Producto> productos = await dal.GetAll<Producto>();
            productos = productos.Where(p => productosVendedor.Select(pv => pv.IdProducto).Contains(p.Id)).ToList();
            productos.ForEach(p => p.Producto_Vendedor = productosVendedor.Where(pv => pv.IdProducto == p.Id).ToList());

            return productos;
        }

        public async Task<bool> ActualizarItemCarrito(ItemCarrito item)
        {
            try
            {
                await dal.Update<ItemCarrito>(item);
                return true;
            } catch (Exception e)
            {
                Console.WriteLine("Error al actualizar el item del carrito: ", e.Message);
                return false;
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
                Console.WriteLine("Error al eliminar el item del carrito: ", e.Message);
                return false;
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
                Console.WriteLine("Error al añadir el item al carrito: ", e.Message);
                return false;
            }
        }

        public async Task<List<Producto_vendedor>> GetAProductoVendedorByProducto(Producto p) 
        {
           List<Producto_vendedor> pvList = await dal.GetAll<Producto_vendedor>();
           return  pvList.Where(pv => pv.IdProducto == p.Id).ToList();
        }
        public async Task<List<Producto>> getProductosListaDeseos()
        {
            try
            {
                List<ListaDeseosItem> listaDeseos = await dal.GetListaDeseos(GetLoggedNickname());
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
                Console.WriteLine("Error al obtener los productos de la Lista de Deseos. " + ex.Message);
                return null;
            }
        }
        public async Task EliminarProductoListaDeseos(Producto_vendedor pv)
        {
            try {
                List<ListaDeseosItem> ListaLoggedUser = await dal.GetListaDeseos(GetLoggedNickname());
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
                Console.WriteLine("Error al eliminar el producto de la Lista de Deseos. " + ex.Message);
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
                Console.WriteLine("Error al añadir el producto a la Lista de Deseos.: " + ex.Message);
            }

        }
        public async Task<Boolean> ProductoEnListaDeseos(Usuario user, Producto_vendedor pv)
        {
            List<ListaDeseosItem> listaDeseos = await dal.GetListaDeseos(user.Nickname);
            ListaDeseosItem item = listaDeseos.Where(i => i.ProductoVendedorId == pv.Id).FirstOrDefault();
            return item != null;
        }
        public async Task<List<Producto>> GetProductosGuardarMasTarde()
        {
            string nickPropietario = GetLoggedNickname();
            List<int> productoslistaMasTarde = new List<int>();
            List<Producto> listaMasTarde = new List<Producto>();
            try
            {
                productoslistaMasTarde = await GetProductosIdGuardarMasTarde(nickPropietario);
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
                Console.WriteLine("Error al eliminar el producto de la Lista Guardar para más tarde. " + ex.Message);
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
                Console.WriteLine("Error al añadir el producto a la Lista Guardar para más tarde.: " + ex.Message);
            }

        }
        public async Task<Boolean> ProductoEnGuardarMasTarde(Producto producto)
        {
            string propietario = GetLoggedNickname();
            List<GuardarMasTardeItem> gmt = await GetGuardarMasTarde();
            GuardarMasTardeItem item = gmt.Where(ld => ld.ProductoId == producto.Id).FirstOrDefault();
            return item != null;
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
                Console.WriteLine("Error al obtener el carrito: ", e.Message);
                return null;
            }
        }
        public async Task<List<int>> GetProductosIdGuardarMasTarde(string nickPropietario)
        {
            try
            {
                List<GuardarMasTardeItem> productosGuardarMasTarde = await dal.GetAll<GuardarMasTardeItem>();
                return productosGuardarMasTarde.Where(p => p.NickPropietario == loggedUser.Nickname).Select(gmt => gmt.ProductoId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener el carrito: ", e.Message);
                return null;
            }
        }
        public Usuario GetUsuarioLogueado()
        {
            return loggedUser;
        }
    }
}
