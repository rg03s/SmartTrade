using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Fabrica
{
    public class RopaFactory : ProductoFactory
    {
        public override Producto CrearProducto(params object[] args)
        {
            if (args.Length != 4) throw new ArgumentOutOfRangeException("Número de argumentos incorrecto!");
            else return new Ropa { Talla = (string)args[0], Color = (string)args[1], Marca = (string)args[2], TipoPrenda = (string)args[3] };
        }
    }
}
