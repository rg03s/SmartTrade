using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartTrade.Logica.Services
{
    public partial interface ISTService
    {
        Task<bool> Login(string nickname, string password);
        Usuario GetUsuarioLogueado();
        Task<List<Producto>> GetProductosPorCategoria(string categoria);
        Task<List<ItemCarrito>> GetCarrito();
        Task<Producto> GetProductoByIdProductoVendedor(int idProductoVendedor);
        Task<List<Producto>> GetAllProductos();
        Task<bool> ActualizarItemCarrito(ItemCarrito itemCarrito);
        Task<bool> EliminarItemCarrito(ItemCarrito itemCarrito);
        string GetLoggedNickname();
        bool IsVendedor();
        Task<List<Producto>> GetProductosDeVendedor(string nickname);
        Task<bool> AgregarItemCarrito(ItemCarrito itemCarrito);
        Task<List<Producto>> getProductosListaDeseos();
        Task EliminarProductoListaDeseos(Producto_vendedor pv);
        Task AgregarProductoListaDeseos(Producto_vendedor pv);
        Task<Boolean> ProductoEnListaDeseos(Usuario user, Producto_vendedor pv);
        Task<List<Producto_vendedor>> GetAProductoVendedorByProducto(Producto p);
        Task<List<Pedido>> GetPedidos();
        Task CargarDetallesPedido(Pedido pedido);
        ImageSource GetImagenPedido(Pedido pedido);
        string GetNombreProductoPedido(int idProducto);
        string GetDescripcionProductoPedido(int idProducto);
       Task<Producto_vendedor> GetProductoVendedorById(int id);
       Task CancelarPedido(Pedido pedido);
       Task DevolverPedido(Pedido pedido);
       void ActualizarEstadoPedido(Pedido pedido);
       Task<List<Producto>> GetProductosPedido(Pedido pedido);
    }
}
