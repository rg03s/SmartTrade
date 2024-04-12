using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Entities
{
    public partial class Comprador : Usuario
    {
        public Comprador() { }

        public Comprador(string nickname, string nombre, string contraseña, string direccion, string email, DateTime fecha_nac, int puntos) { 
            this.Puntos = puntos;
        }
    }
}
