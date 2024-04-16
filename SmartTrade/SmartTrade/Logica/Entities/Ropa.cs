using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Ropa : Producto
    {
        public Ropa() { }
        
        public Ropa(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos,string cat, string talla, string color, string marca, string tipoPrenda)
            :base(nombre, huella, imagen, modelo3d, desc, puntos,cat)
        {
            this.Talla = talla;
            this.Color = color;
            this.Marca = marca;
            this.TipoPrenda = tipoPrenda;
        }

        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, string talla, string color, string marca, string tipoPrenda)
        {
            return new Ropa(nombre, huella, imagen, modelo3d, desc, puntos, cat, talla, color, marca, tipoPrenda);
        }
    }
}
