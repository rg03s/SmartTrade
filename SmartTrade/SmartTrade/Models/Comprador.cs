using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Supabase;

namespace SmartTrade.Models
{
    public partial class Comprador 
    {
        public string Nickname { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }

        public string Direccion { get; set; }

        public string Email { get; set; }

        public DateTime Fecha_nac { get; set; }

        public int Puntos { get; set; }

    }
}
