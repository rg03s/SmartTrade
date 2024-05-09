using System;
using System.Collections.Generic;
using System.Text;
using SmartTrade.Entities;

namespace SmartTrade.Fabrica
{
    public interface IProductoFactory
    {
        Producto CrearProducto();
    }
}
