using SmartTrade.Logica.Estrategia;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Pedido
    {
        public Pedido() { }
        public Pedido(DateTime fecha, double precio_total, List <int> IdProductosCarrito, string nickcomprador, string direccion, string num_tarjeta, int puntos_obtenidos, string estado)
        {
            this.Fecha = fecha;
            this.Precio_total = precio_total;
            this.NickComprador = nickcomprador;
            this.Direccion = direccion;
            this.IdProductoVendedor = IdProductosCarrito;
            this.Num_tarjeta = num_tarjeta;
            this.Puntos_obtenidos = puntos_obtenidos;
            this.Estado = estado;
        }
        public void EstablecerEstrategiaPago(IEstrategiaPago estrategiaPago)
        {
            this.estrategiaPago = estrategiaPago;
        }
        public void Pagar()
        {

            if (estrategiaPago != null)
            {
                estrategiaPago.pagar();
            }
            else
            {
                Console.WriteLine("No se ha establecido una estrategia de pago.");
            }
        }
    }
}
