using SmartTrade.Logica.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Tecnologia : Producto
    {
        public Tecnologia() { }
        
        public Tecnologia(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, string dispositivo, string marca, string modelo)
            :base(nombre, huella, imagen, modelo3d, desc, puntos, cat)
        {
            this.Dispositivo = dispositivo;
            this.Marca = marca;
            this.Modelo = modelo;
        }

        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, string dispositivo, string marca, string modelo)
        {
            return new Tecnologia(nombre, huella, imagen, modelo3d, desc, puntos, cat, dispositivo, marca, modelo);
        }
    }
}
