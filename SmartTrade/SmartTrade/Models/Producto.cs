using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartTrade.Models
{
    public partial class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Huella_eco { get; set; }

        public Image Imagen { get; set; }

        public Image Modelo3d { get; set; }

        public string Descripcion { get; set; }

        public int Puntos { get; set; }
    
        public ICollection<Producto_vendedor> Producto_vendedores { get;}
    }
}
