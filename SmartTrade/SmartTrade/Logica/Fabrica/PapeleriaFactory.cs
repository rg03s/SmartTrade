using System;
using System.Collections.Generic;
using System.Text;
using SmartTrade.Entities;

namespace SmartTrade.Fabrica
{
    public class PapeleriaFactory : ProductoFactory
    {
        public override Producto CrearProducto(params object[] args)
        {
            if (args.Length != 1) { throw new ArgumentOutOfRangeException("Número de argumentos incorrecto!"); }
            else return new Papeleria { Material = (string)args[0] };
        }
    }
}
