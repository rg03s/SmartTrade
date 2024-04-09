using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Ropa : Producto
    {
        public int Talla { get; set; }

        public string Color { get; set; }
    }
}
