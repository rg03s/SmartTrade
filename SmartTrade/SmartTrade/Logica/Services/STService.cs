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
        private SupabaseContext supabaseContext = SupabaseContext.Instance;
        private static STService instance = new STService(new STDAL(new SupabaseContext()));
        private Usuario loggedUser;

        public STService(IDAL dal)
        {
            this.dal = dal;
        }
        public static STService Instance
        {
            get
            {
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


        /*
        public void Commit()
        {
            dal.Commit();
        }

        public void AddUser(Usuario usuario)
        {
           
            
                dal.Insert<Usuario>(usuario);
                dal.Commit();
                

            
        }

        public void GetUsuarios() {
            dal.GetAll<Usuario>();
        }
        */


        /*public async Task<bool> Login(string nickname, string password)
        {
            Usuario usuario = await dalUsuario.GetById(nickname);
            Usuario correo = dalUsuario.GetByEmail(usuario.Email);
            // Si no existe el usuario
            if (usuario == null || correo == null)
            {
                return false;
            }

            // Si la contraseña no coincide
            else if (usuario.Password != password)
            {
                return false;
            }

            else
            {
                loggedUser = usuario;
                return true;
            }

        }*/

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

        public async Task<List<Producto>> GetAllProductsAsync()
        {
            var productos = await dal.GetAll<Producto>();
            return productos.ToList();
        }

        public async Task<List<Deporte>> GetAllDeporte()
        {
            var d = await dal.GetAll<Deporte>();
            return d.ToList();
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

                return productos;
            }
            catch (Exception e) { 
                Console.WriteLine("Error al obtener los productos: ", e.Message);
                return null; 
            }
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

        public Producto_vendedor GetProductoVendedorById(int id)
        {
            try
            {
                return dal.GetById<Producto_vendedor>(id.ToString()).Result;
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
            string nickPropietario = GetLoggedNickname();
            List<int> productosListaDeseos = new List<int>();
            List<Producto> listaDeseos = new List<Producto>();
            try
            {   productosListaDeseos = await dal.GetProductosIdLista(nickPropietario);
                if (productosListaDeseos != null)
                {
                    Producto p = null;

                    foreach (int idproducto in productosListaDeseos)
                    {
                        p = await dal.GetById<Producto>(idproducto);
                        if (p != null)
                        {
                            listaDeseos.Add(p);
                        }
                    }
                }
                
                return listaDeseos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los productos de la Lista de Deseos. " + ex.Message);
                return null;
            }
        }
        public async Task EliminarProductoListaDeseos(Producto productoLista)
        {
            try {
                List<ListaDeseosItem> ListaLoggedUser = await dal.GetListaDeseos(GetLoggedNickname());
                foreach (var item in ListaLoggedUser)
                {
                    if (item.ProductoId == productoLista.Id) await dal.Delete<ListaDeseosItem>(item);
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el producto de la Lista de Deseos. " + ex.Message);
            }
            
        }

        public async Task AgregarProductoListaDeseos(Producto producto)
        {
            string propietario = GetLoggedNickname();
            ListaDeseosItem ld = new ListaDeseosItem(propietario, producto.Id);
            bool estaEnLista = await ProductoEnListaDeseos(producto);
            if (!estaEnLista) await dal.Add<ListaDeseosItem>(ld);
            else;
        }
        //true si el producto ya está en la lista
        public async Task<Boolean> ProductoEnListaDeseos(Producto producto)
        {
            string propietario = GetLoggedNickname();
            List<ListaDeseosItem> listaDeseos = await dal.GetListaDeseos(propietario);
            ListaDeseosItem item = listaDeseos.Where(ld => ld.ProductoId == producto.Id).FirstOrDefault();
            return item != null;
        }

            public Usuario GetUsuarioLogueado()
        {
            return loggedUser;
        }
    }
}
