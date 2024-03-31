using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Valoracion
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double Nota { get; set; }

        public string Comentario { get; set; }

        public Producto Producto { get; set; }

        public Comprador Comprador { get; set; }


    }
}
