using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class ListaDeseosItem
    {
        public ListaDeseosItem() { }

        public ListaDeseosItem(string nickPropietario, int productoId)
        {
            this.NickPropietario = nickPropietario;
            this.ProductoId = productoId;
        }
    }
}
