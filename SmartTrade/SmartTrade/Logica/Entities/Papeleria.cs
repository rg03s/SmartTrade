using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Papeleria : Producto
    {
        public Papeleria() { }
        
        public Papeleria(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, int precio, string material)
            :base(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio)
        {
            this.Material = material;
        }

        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, int precio, string material)
        {
            return new Papeleria(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, material);
        }
    }
}
