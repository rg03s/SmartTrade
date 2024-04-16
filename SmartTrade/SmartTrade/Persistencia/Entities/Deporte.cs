using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    [Table("Deporte")]
    public partial class Deporte : Producto
    {
        [Column("tipo")]
        public string Tipo { get; set; }
    }
}
