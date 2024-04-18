using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    [Table("Categoria")]
    public partial class Categoria
    {

        [Key]
        [Column("id")]
        public string Nombre { get; set; }

    }
}
