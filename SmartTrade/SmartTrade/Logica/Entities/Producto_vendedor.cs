using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Producto_vendedor
    {
        public Producto_vendedor() { }

        public Producto_vendedor(int idProducto, string vendedor, int stock, double precio)
        {
            this.IdProducto = idProducto;
            this.nicknameVendedor = vendedor;
            this.Stock = stock;
            this.Precio = precio;
        }
    }
}
