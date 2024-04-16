using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Deporte : Producto
    {
        public Deporte() { }
        
        public Deporte(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Categoria cat, string tipo)
            :base(nombre, huella, imagen, modelo3d, desc, puntos, cat)
        {
            this.Tipo = tipo;
        }

        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Categoria cat, string tipo)
        {
            return new Deporte(nombre, huella, imagen, modelo3d, desc, puntos, cat, tipo);
        }
    }
}
