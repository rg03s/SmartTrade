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
        [Column("id_ProductoVendedor")]
        public List<int> IdProductoVendedor { get; set; }
        [Column("id_comprador")]
        public string NickComprador{ get; set; }
        [Column("direccion_entrega")]
        public string Direccion { get; set; }
        [Column("tarjeta")]
        public string Num_tarjeta { get; set; }
        [Column ("puntos")]
        public int Puntos_obtenidos { get; set; }
        [Column("estado")]
        public string Estado { get; set; }
    }
}
