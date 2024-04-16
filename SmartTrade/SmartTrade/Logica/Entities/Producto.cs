using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartTrade.Entities
{
    public partial class Producto
    {
        public Producto() { }
        public Producto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string idCat)
        {
            this.Nombre = nombre;
            this.Huella_eco = huella;
            this.Imagen = imagen;
            this.Modelo3d = modelo3d;
            this.Descripcion = desc;
            this.Puntos = puntos;
            this.IdCategoria = idCat;
            this.Producto_vendedores = new List<Producto_vendedor>();
            this.Stock = 0;
            //ARREGLAR. TIRA EXCEPCION
            //Producto_vendedor pv = new Producto_vendedor(this, vend, stock, precio);
            //this.Producto_vendedores.Add(pv);
            
        }
    }
}
