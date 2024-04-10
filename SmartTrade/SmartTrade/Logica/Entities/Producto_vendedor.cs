using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Producto_vendedor
    {
        public Producto_vendedor() { }

        public Producto_vendedor(Producto producto, Vendedor vendedor, int stock, int precio)
        {
            this.Producto = producto;
            this.Vendedor = vendedor;
            this.Stock = stock;
            this.Precio = precio;
        }
    }
}
