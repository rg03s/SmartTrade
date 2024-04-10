using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Entities
{
    public interface IFabrica
    {
        Producto CrearProducto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, Vendedor vend, Categoria cat, int stock, double precio, Dictionary<string, object> atributosEspecificos);
    }

}
