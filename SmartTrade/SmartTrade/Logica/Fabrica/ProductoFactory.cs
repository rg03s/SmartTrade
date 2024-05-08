using System;
using System.Collections.Generic;
using System.Text;
using SmartTrade.Entities;

namespace SmartTrade.Fabrica
{
    public abstract class ProductoFactory
    {
        public abstract Producto CrearProducto(params object[] args);
    }
}
