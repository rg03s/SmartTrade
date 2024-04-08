using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartTrade.Entities
{
    public partial class Producto
    {
        public Producto(string Nombre, string Huella_eco, Image Imagen, string Descripcion) {
            this.Nombre = Nombre;
            this.Huella_eco = Huella_eco;
            this.Imagen = Imagen;
            this.Descripcion = Descripcion;
        }
    }
}
