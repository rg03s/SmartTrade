using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Logica.Services
{
    public partial interface ISTService
    {
        /*
        void Commit();
        void AddUser(Usuario u);

        void GetUsuarios();
        */
        Task<bool> Login(string nickname, string password);

        Task<List<Producto>> GetProductosPorCategoria(string categoria);
        Task<List<ItemCarrito>> GetCarrito();
        Producto GetProductoById(string idProducto);
        Task<Producto> GetProductoByIdProductoVendedor(int idProductoVendedor);
        Producto_vendedor GetProductoVendedorById(int idProductoVendedor);
    }
}
