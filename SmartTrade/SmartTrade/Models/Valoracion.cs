using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Models
{
    public partial class Valoracion
    {
        public int Id { get; set; }

        public DateTime fecha { get; set; }

        public double nota { get; set; }

        public string comentario { get; set; }

        public Producto producto { get; set; }

        public Comprador comprador { get; set; }    


    }
}
