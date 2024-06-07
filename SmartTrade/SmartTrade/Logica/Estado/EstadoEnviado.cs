using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Estado
{
    public class EstadoEnviado : IEstadoPedido
    {
        Pedido pedido;
        public EstadoEnviado(Pedido p)
        {
            this.pedido = p;
        }

        public void Transicion()
        {
            this.pedido.Estado = "Pedido enviado";
        }
    }
}
