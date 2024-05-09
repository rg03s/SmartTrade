using System;
using System.Collections.Generic;
using System.Text;
using SmartTrade.Entities;

namespace SmartTrade.Fabrica
{
    public class PapeleriaFactory : IProductoFactory
    {
        private string nombre, huella, imagen, modelo3d, desc, cat, material;
        private int puntos;

        public PapeleriaFactory(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, string material)
        {
            this.nombre = nombre;
            this.huella = huella;
            this.imagen = imagen;
            this.modelo3d = modelo3d;
            this.desc = desc;
            this.puntos = puntos;
            this.cat = cat;
            this.material = material;
        }

        public Producto CrearProducto()
        {
            return new Papeleria(nombre, huella, imagen, modelo3d, desc, puntos, cat, material);
        }
    }
}
