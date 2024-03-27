using System;
using System.Collections.Generic;

namespace SmartTrade.Models
{
    public partial class Vendedor
    {
        public string Nickname { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }

        public string Direccion { get; set; }

        public string Email { get; set; }

        public DateTime Fecha_nac {  get; set; }

        public string Cuenta_bancaria { get; set; }

        public ICollection<Producto_vendedor> Productos_vendedor { get; set; }
    }
}