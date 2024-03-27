using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Models
{
    public partial class Tarjeta
    {
        public int Numero { get; set; }

        public DateTime Fecha_cad {  get; set; }

        public int Num_seguridad { get; set; }

        public string Nombre { get; set; }

        public Comprador Comprador { get; set; }


    }
}
