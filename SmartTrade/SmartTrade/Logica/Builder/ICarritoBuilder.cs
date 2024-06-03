using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Logica.CarritoBuilder
{
    public interface IBuilder
    {
        Task SetProducts(List<ItemCarrito> p);
        void Reset();   
    }
}
