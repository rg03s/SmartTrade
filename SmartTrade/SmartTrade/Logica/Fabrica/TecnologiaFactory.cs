using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Fabrica
{
    public class TecnologiaFactory : IProductoFactory
    {
        private string nombre, huella, imagen, modelo3d, desc, cat, dispositivo, marca, modelo;
        private int puntos;

        public TecnologiaFactory(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat, string dispositivo, string marca, string modelo)
        {
            this.nombre = nombre;
            this.huella = huella;
            this.imagen = imagen;
            this.modelo3d = modelo3d;
            this.desc = desc;
            this.puntos = puntos;
            this.cat = cat;
            this.dispositivo = dispositivo;
            this.marca = marca;
            this.modelo = modelo;
        }

        public Producto CrearProducto()
        {
            return new Tecnologia(nombre, huella, imagen, modelo3d, desc, puntos, cat, dispositivo, marca, modelo);
        }
    }
}
