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
        public Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, int precio, Dictionary<string, object> atributosEspecificos)
        {
            switch (cat.Nombre.ToLower())
            {
                case "deporte":
                    string tipo = atributosEspecificos["tipo"] as string;
                    return new Deporte(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, tipo);
                case "ropa":
                    string talla = atributosEspecificos["talla"] as string;
                    string color = atributosEspecificos["color"] as string;
                    string marca = atributosEspecificos["marca"] as string;
                    string tipoPrenda = atributosEspecificos["tipoPrenda"] as string;
                    return new Ropa(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, talla, color, marca, tipoPrenda);
                case "papeleria":
                    string material = atributosEspecificos["material"] as string;
                    return new Papeleria(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, material);
                case "tecnologia":
                    string dispositivo = atributosEspecificos["dispositivo"] as string;
                    string marca_dispo = atributosEspecificos["marca"] as string;
                    string modelo = atributosEspecificos["modelo"] as string;
                    return new Tecnologia(nombre, huella, imagen, modelo3d, desc, puntos, vend, cat, stock, precio, dispositivo, marca_dispo, modelo);
                default:
                    throw new ArgumentException("Categoría del producto desconocida o no disponible");
            }
        }
    }

}
