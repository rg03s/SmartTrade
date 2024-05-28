using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Tarjeta
    {
        public Tarjeta() { }

        public Tarjeta(string numero, DateTime fecha_cad, string num_seguridad, string nick_comprador) 
        {
            this.Numero = numero;
            this.Fecha_cad = fecha_cad;
            this.Num_seguridad = num_seguridad;
            this.Nick_comprador = nick_comprador;
        }
    }
}
