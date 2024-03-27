using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Models
{
    public partial class Producto_vendedor
    {
        public int Id { get; set; }

        public double Precio { get; set; }

        public int Stock {  get; set; }

        public Vendedor Vendedor { get; set; }

        public Producto Producto { get; set; }
    }
}
