using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Producto
    {
        public Producto() {
            this.Producto_Vendedor = new List<Producto_vendedor>();
        }
        public Producto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat)
        {
            this.Nombre = nombre;
            this.Huella_eco = huella;
            this.Imagen = imagen;
            this.Modelo3d = modelo3d;
            this.Descripcion = desc;
            this.Puntos = puntos;
            this.Categoria = cat;
            
        }
    }
}
