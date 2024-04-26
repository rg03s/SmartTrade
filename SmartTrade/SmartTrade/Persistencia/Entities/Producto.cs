
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartTrade.Entities
{
    [Table("Producto")]
    public partial class Producto
    {
        [Key]
        [Column("id")]
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
        [NotMapped]
        public virtual ICollection<Producto_vendedor> Producto_Vendedor { get; set; }
        [Column("categoria")]
        public string Categoria { get; set; }
        
    }
}
