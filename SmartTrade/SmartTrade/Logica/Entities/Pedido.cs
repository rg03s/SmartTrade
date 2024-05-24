using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Pedido
    {
        public Pedido() { }
        public Pedido(DateTime fecha, double precio_total, List <int> itemsCarrito, string nickcomprador, string direccion, int num_tarjeta, int puntos_obtenidos, string estado)
        {
            this.Fecha = fecha;
            this.Precio_total = precio_total;
            this.NickComprador = nickcomprador;
            this.Direccion = direccion;
            this.ItemsCarrito = itemsCarrito;
            this.Num_tarjeta = num_tarjeta;
            this.Puntos_obtenidos = puntos_obtenidos;
            this.Estado = estado;
        }
    }
}
