using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Ropa : Producto
    {
        public string Talla { get; set; }

        public string Color { get; set; }
        public string Marca { get; set; }
        public string TipoPrenda { get; set; }

    }
}
