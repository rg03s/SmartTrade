using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Entities
{
    public partial class Usuario
    {
        public Usuario() { }
        public Usuario(string nickname, string nombre, string password, string direccion, string email, DateTime fecha_nac)
        {
            this.Nickname = nickname;
            this.Nombre = nombre;
            this.Password = password;
            this.Direccion = direccion;
            this.Email = email;
            this.Fecha_nac = fecha_nac;
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
    }
}
