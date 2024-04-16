using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SmartTrade.Entities
{
     [Table("Tecnologia")]
    public partial class Tecnologia : Producto
    {
        [Column("dispositivo")]
        public string Dispositivo { get; set; }
        [Column("marca")]
        public string Marca { get; set; }
        [Column("modelo")]
        public string Modelo { get; set; }
    }
}
