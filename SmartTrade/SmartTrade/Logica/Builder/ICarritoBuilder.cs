using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.CarritoBuilder
{
    public interface IBuilder
    {
        void SetProducts(List<ItemCarrito> p);
        void Reset();   
    }
}
