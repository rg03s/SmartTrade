using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Fabrica
{
    public class TecnologiaFactory : ProductoFactory
    {
        public override Producto CrearProducto(params object[] args)
        {
            if (args.Length != 3) throw new ArgumentOutOfRangeException("Número de argumentos incorrecto!");
            else return new Tecnologia { Dispositivo = (string)args[0], Marca = (string)args[1], Modelo = (string)args[2] };
        }
    }
}
