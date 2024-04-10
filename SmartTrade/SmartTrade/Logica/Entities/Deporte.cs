using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Deporte : Producto
    {
        public Deporte() { }
        
        public Deporte(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, double precio, string tipo)
            :base(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio)
        {
            this.Tipo = tipo;
        }

        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, double precio, string tipo)
        {
            return new Deporte(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, tipo);
        }
    }
}
