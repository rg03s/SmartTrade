using SmartTrade.Logica.Observador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Entities
{
    public partial class Usuario : IObservador
    {
        public Usuario() { 
            AlertasProductosSinStock = new List<Producto>();
        }
        public Usuario(string nickname, string nombre, string password, string direccion, string email, DateTime fecha_nac, Boolean isVendedor)
        {
            this.Nickname = nickname;
            this.Nombre = nombre;
            this.Password = password;
            this.Direccion = direccion;
            this.Email = email;
            this.Fecha_nac = fecha_nac;
            this.IsVendedor = isVendedor;
        }

        public void AddDatosVendedor(string cuenta_bancaria)
        {
            if (this.IsVendedor)
            {
                this.Cuenta_bancaria = cuenta_bancaria;
                this.Productos_vendedor = new List<Producto_vendedor>();
            }
        }

        public void AddDatosComprador()
        {
            this.Puntos = 0;
        }
        public override string ToString()
        {
            return $"Nombre: {Nombre}, Email: {Email}";
        }


        public Boolean IsContraseñaValida(string contraseña) 
        {

            if (string.IsNullOrEmpty(contraseña) || contraseña.Length < 8) return false;
            return contraseña.Any(char.IsDigit);
        }

        public void Actualizar(Producto p)
        {
            if (!AlertasProductosSinStock.Contains(p))
            {
                AlertasProductosSinStock.Add(p);
            }
        }
    }
}
