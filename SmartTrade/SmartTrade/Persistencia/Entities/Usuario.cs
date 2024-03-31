using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Usuario
    {
        public string Nickname { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }

        public string Direccion { get; set; }

        public string Email { get; set; }

        public DateTime Fecha_nac { get; set; }

    }
}
