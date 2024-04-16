using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartTrade.Entities
{
    [Table("Ropa")]
    public partial class Ropa : Producto
    {
        [Column("talla")]
        public string Talla { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("marca")]
        public string Marca { get; set; }
        [Column("tipoPrenda")]
        public string TipoPrenda { get; set; }

    }
}
