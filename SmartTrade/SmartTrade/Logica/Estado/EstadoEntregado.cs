using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Estado
{
    internal class EstadoEntregado : IEstadoPedido
    {
        Pedido pedido;
        public EstadoEntregado(Pedido p)
        {
            this.pedido = p;
        }

        public void Transicion()
        {
            this.pedido.Estado = "Pedido entregado";
        }
    }
}
