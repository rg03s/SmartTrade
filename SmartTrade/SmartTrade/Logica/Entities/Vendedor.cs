using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Entities
{
    public partial class Vendedor : Usuario
    {
        public Vendedor() {}

        public Vendedor(string nickname, string nombre, string contraseña, string direccion, string email, DateTime fecha_nac, string cuenta_bancaria) : base(nickname, nombre, contraseña, direccion, email, fecha_nac)
        {
            this.Cuenta_bancaria = cuenta_bancaria;

            this.Productos_vendedor = new List<Producto_vendedor>();
        }
    }    
}
