using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class ListaDeseosItem
    {
        public ListaDeseosItem() { }

        public ListaDeseosItem(string nickPropietario, int productoVendedorId)
        {
            this.NickPropietario = nickPropietario;
            this.ProductoVendedorId = productoVendedorId;
        }
    }
}
