using SmartTrade.Entities;
using SmartTrade.Fabrica;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Fabrica
{
    public class DeporteFactory : IProductoFactory
    {
        private string nombre, huella, imagen, modelo3d, desc, cat, tipo;
        private int puntos;

        public DeporteFactory(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, string tipo)
        {
            this.nombre = nombre;
            this.huella = huella;
            this.imagen = imagen;
            this.modelo3d = modelo3d;
            this.desc = desc;
            this.puntos = puntos;
            this.cat = cat;
            this.tipo = tipo;
        }

        public Producto CrearProducto()
        {
            return new Deporte(nombre, huella, imagen, modelo3d, desc, puntos, cat, tipo);
        }
    }
}
