using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartTrade.Logica.Entities
{
    public class Fabrica : IFabrica<Producto>
    {
        public Producto CrearProducto(params object[] args)
        {
            Type type = typeof(Producto);
            Type[] parameterTypes = args.Select(arg => arg.GetType()).ToArray();
            ConstructorInfo constructor = type.GetConstructor(parameterTypes);

            if (constructor == null)
            {
                throw new ArgumentException("No se encontró un constructor compatible.");
            }

            // Convierte cada argumento al tipo adecuado
            object[] parameters = args.Select(arg => Convert.ChangeType(arg, arg.GetType())).ToArray();

            //invoca el constructor del producto
            return (Producto)constructor.Invoke(parameters);
        }
    }
}
