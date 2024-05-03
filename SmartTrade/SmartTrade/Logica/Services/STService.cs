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
using Xamarin.Essentials;

namespace SmartTrade.Logica.Services
{
    public class STService : ISTService
    {
        private readonly IDAL<Usuario> dalUsuario;
        private readonly IDAL<Vendedor> dalVendedor;
        private readonly IDAL<Comprador> dalComprador;
        private readonly IDAL<Tarjeta> dalTarjeta;
        private readonly IDAL<Producto> dalProducto;
        private readonly IDAL<Deporte> dalDeporte;
        private readonly IDAL<Producto_vendedor> dalProductoVendedor;
        private readonly IDAL<ItemCarrito> dalCarrito;
        private SupabaseContext supabaseContext = SupabaseContext.Instance;
        private static STService instance = new STService();
        private Usuario loggedUser;

        public STService()
        {
            dalUsuario = new STDAL<Usuario>(supabaseContext);
            dalProducto = new STDAL<Producto>(supabaseContext);
            dalProductoVendedor = new STDAL<Producto_vendedor>(supabaseContext);
            dalCarrito = new STDAL<ItemCarrito>(supabaseContext);
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
            if (dalUsuario.GetByEmail(usuario.Email) != null)
            {
                throw new EmailYaRegistradoException();
            }
            else if (dalUsuario.GetById(usuario.Nickname) != null)
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
                        await dalUsuario.Add(usuario);
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

            await dalVendedor.Add(vendedor);
        }

        public async Task AddUserComprador(Comprador comprador)
        {
            await dalComprador.Add(comprador);
        }

        public async Task AddTarjeta(Tarjeta tarjeta)
        {
            await dalTarjeta.Add(tarjeta);
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
                Usuario usuario = dalUsuario.GetById(identifier);
                if (usuario == null)
                {
                    usuario = dalUsuario.GetByEmail(identifier);
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
            var productos = await dalProducto.GetAll();
            return productos.ToList();
        }

        public async Task<List<Deporte>> GetAllDeporte()
        {
            var d = await dalDeporte.GetAll();
            return d.ToList();
        }

        // metodo para ordenar los productos por categorías
        public async Task<List<Producto>> GetProductosPorCategoria(string categoria)
        {
            var productos = await dalProducto.GetAll();
            var productosPorCategoria = productos.Where(p => p.Categoria == categoria);
            return productosPorCategoria.ToList();
        }

        public async Task<List<Producto>> GetAllProductos()
        {
            try {
                List<Producto> productos = await dalProducto.GetAll();
                
                List<Producto_vendedor> productoVendedor = await dalProductoVendedor.GetAll();
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
                return dalProducto.GetById(id);
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
                List<Producto_vendedor> productoVendedorList = await dalProductoVendedor.GetAll();
                Producto_vendedor pv = productoVendedorList.Where(aux => aux.Id == idProductoVendedor).FirstOrDefault();

                if (pv == null)
                {
                    Console.WriteLine("Producto vendedor no encontrado");
                    return null;
                }
                else
                {
                    //No funciona el getById del DAL
                    List<Producto> productos = await dalProducto.GetAll();
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
                return dalProductoVendedor.GetById(id.ToString());
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
                List<ItemCarrito> productosCarrito = await dalCarrito.GetAll();
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
            List<Producto_vendedor> productosVendedor = await dalProductoVendedor.GetAll();
            List<Producto> productos = new List<Producto>();

            productosVendedor = productosVendedor.Where(pv => pv.NicknameVendedor == loggedUser.Nickname).ToList();
            foreach (Producto_vendedor pv in productosVendedor)
            {
                productos.Add(dalProducto.GetById(pv.IdProducto.ToString()));
            }

            return productos;
        }
    }
}
