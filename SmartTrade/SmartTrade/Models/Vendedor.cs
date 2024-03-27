using System;
using System.Collections.Generic;

namespace SmartTrade.Models
{
    public partial class Vendedor
    {
        public string nickname { get; set; }
        public string nombre { get; set; }
        public string contraseña { get; set; }

        public string direccion { get; set; }

        public string email { get; set; }

        public DateTime fecha_nac {  get; set; }

        public string cuenta_bancaria { get; set; }

        public ICollection<Producto_vendedor> Productos_vendedor { get; set; }
    }
}