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

        public ItemCarrito(int idProductoVendedor, int cantidad, Usuario usuario, string caracteristica)
        {
            this.idProductoVendedor = idProductoVendedor;
            this.Cantidad = cantidad;
            this.NicknameUsuario = usuario.Nickname;
            this.Caracteristica = caracteristica;
        }
    }
}
