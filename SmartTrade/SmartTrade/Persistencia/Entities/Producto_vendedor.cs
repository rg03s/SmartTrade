using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    [Table("Producto_vendedor")]
    public partial class Producto_vendedor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("precio")]
        public double Precio { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("vendedor")]
        public string NicknameVendedor { get; set; }

        [Column("producto")]
        public int IdProducto{ get; set; }
    }
}
