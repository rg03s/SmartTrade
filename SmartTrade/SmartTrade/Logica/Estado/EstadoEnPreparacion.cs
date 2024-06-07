using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Estado
{
    internal class EstadoEnPreparacion : IEstadoPedido
    {
        Pedido pedido;
        public EstadoEnPreparacion(Pedido p)
        {
            this.pedido = p;
        }

        public void Transicion()
        {
            this.pedido.Estado = "Pedido en preparación";
        }
    }
}
