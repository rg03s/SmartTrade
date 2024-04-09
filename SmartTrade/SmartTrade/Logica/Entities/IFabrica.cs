using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Entities
{
    public interface IFabrica<T> where T : Producto, new()
    {
        T CrearProducto(params object[] args);
    }

}
