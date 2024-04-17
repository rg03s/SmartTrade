﻿using SmartTrade.Entities;
using SmartTrade.Fabrica;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Fabrica
{
    public class DeporteFactory : ProductoFactory
    {
        public override Producto CrearProductoVacio()
        {
            return new Deporte();
        }

        public override Producto CrearProducto(params object[] args)
        {
            if (args.Length != 1) throw new ArgumentOutOfRangeException("Número de argumentos incorrecto!");
            return new Deporte { Tipo = (string)args[0] };
        }
    }
}