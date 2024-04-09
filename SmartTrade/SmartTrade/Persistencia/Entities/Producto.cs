using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartTrade.Entities
{
    public partial class Producto
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("huella_ecologica")]
        public string Huella_eco { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
        [Column("modelo3d")]
        public string Modelo3d { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
        [Column("puntos")]
        public int Puntos { get; set; }
        [Column("producto_vendedores")]
        public ICollection<Producto_vendedor> Producto_vendedores { get; }
        public Categoria Categoria { get; set; }
    }
}
