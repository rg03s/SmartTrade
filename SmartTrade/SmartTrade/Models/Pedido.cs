using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Models
{
    public partial class Pedido
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double Precio_total { get; set; }

        public ICollection<Producto> productos { get; set; }

        public Comprador comprador { get; set; }
    }
}
