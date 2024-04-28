using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class ItemCarrito
    {
        public ItemCarrito()
        {
        }

        public ItemCarrito(int idProductoVendedor, int cantidad, DateTime created_at)
        {
            this.idProductoVendedor = idProductoVendedor;
            this.Cantidad = cantidad;
            this.CreatedAt = created_at;
        }
    }
}
