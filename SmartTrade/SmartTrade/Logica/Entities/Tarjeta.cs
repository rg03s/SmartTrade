using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Tarjeta
    {
        public Tarjeta() { }

        public Tarjeta(int numero, DateTime fecha_cad, int num_seguridad, string Nombre, string nick_comprador) 
        {
            this.Numero = numero;
            this.Fecha_cad = fecha_cad;
            this.Num_seguridad = num_seguridad;
            this.Nombre = Nombre;
            this.Nick_comprador = nick_comprador;
        }
    }
}
