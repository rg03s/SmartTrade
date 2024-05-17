using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class GuardarMasTardeItem
    {
        public GuardarMasTardeItem() { }

        public GuardarMasTardeItem(string nickPropietario, int productoId)
        {
            this.NickPropietario = nickPropietario;
            this.ProductoId = productoId;
        }
    }
}
