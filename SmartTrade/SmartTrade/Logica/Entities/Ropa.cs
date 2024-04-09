using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Ropa : Producto
    {
        public Ropa() { }
        
        public Ropa(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, int precio, int talla, string color)
            :base(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio)
        {
            this.Talla = talla;
            this.Color = color;
        }

        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, int precio, int talla, string color)
        {
            return new Ropa(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, talla, color);
        }
    }
}
