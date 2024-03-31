using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Pedido
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double Precio_total { get; set; }

        public ICollection<Producto> Productos { get; set; }

        public Comprador Comprador { get; set; }
    }
}
