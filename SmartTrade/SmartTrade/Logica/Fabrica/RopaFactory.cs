using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Fabrica
{
    public class RopaFactory : IProductoFactory
    {
        private string nombre, huella, imagen, modelo3d, desc, cat, talla, color, marca, tipoPrenda;
        private int puntos;

        public RopaFactory(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, 
                                string talla, string color, string marca, string tipoPrenda)
        {
            this.nombre = nombre;
            this.huella = huella;
            this.imagen = imagen;
            this.modelo3d = modelo3d;
            this.desc = desc;
            this.puntos = puntos;
            this.cat = cat;
            this.talla = talla;
            this.color = color;
            this.marca = marca;
            this.tipoPrenda = tipoPrenda;
        }

        public Producto CrearProducto()
        {
            return new Ropa(nombre, huella, imagen, modelo3d, desc, puntos, cat, talla, color, marca, tipoPrenda);
        }
    }
}
