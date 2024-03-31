using System;
using System.Collections.Generic;

namespace SmartTrade.Entities
{
    public partial class Vendedor : Usuario
    {
        public string Cuenta_bancaria { get; set; }

        public ICollection<Producto_vendedor> Productos_vendedor { get; set; }
    }
}