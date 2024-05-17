using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Pedido
    {
        public Pedido() { }
        public Pedido(DateTime fecha, double precio_total, List <int> productos, string nickcomprador)
        {
            this.Fecha = fecha;
            this.Productos = productos;
            this.Precio_total = precio_total;
            this.nickComprador = nickcomprador;
        }
    }
}
