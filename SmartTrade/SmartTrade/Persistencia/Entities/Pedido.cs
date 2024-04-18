using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SmartTrade.Entities
{
    [Table("Pedido")]
    public partial class Pedido
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("fecha")]
        public DateTime Fecha { get; set; }
        [Column("precio_total")]
        public double Precio_total { get; set; }
        [Column("id_productos")]
        public List<int> Productos { get; set; }
        [Column("id_comprador")]
        public string nickComprador{ get; set; }

    }
}
