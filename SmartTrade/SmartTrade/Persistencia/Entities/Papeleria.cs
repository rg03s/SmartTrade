using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    [Table("Papeleria")]
    public partial class Papeleria : Producto
    {
        [Column("material")]
        public string Material { get; set; }
    }
}
