using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartTrade.Logica.Entities
{
    public class Fabrica : IFabrica
    {
        public Producto CrearProducto(string categoria, string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, int precio, Dictionary<string, object> atributosEspecificos)
        {
            switch (categoria.ToLower())
            {
                case "deporte":
                    string tipo = atributosEspecificos["tipo"] as string;
                    return new Deporte(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, tipo);
                case "ropa":
                    int talla = Convert.ToInt32(atributosEspecificos["talla"]);
                    string color = atributosEspecificos["color"] as string;
                    return new Ropa(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, talla, color);
                case "papeleria":
                    string material = atributosEspecificos["material"] as string;
                    return new Papeleria(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, material);
                case "tecnologia":
                    string dispositivo = atributosEspecificos["dispositivo"] as string; 
                    return new Tecnologia(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, dispositivo);
                default:
                    throw new ArgumentException("Categoría del producto desconocida o no disponible");
            }
        }
    }

}
