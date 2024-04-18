using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTrade.Entities
{
    [Table("Valoración")]
    public partial class Valoracion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("fecha")]
        public DateTime Fecha { get; set; }
        [Column("nota")]
        public double Nota { get; set; }
        [Column("comentario")]
        public string Comentario { get; set; }
        [Column("id_producto")]
        public int Producto { get; set; }
        [Column("id_comprador")]
        public string nickComprador { get; set; }


    }
}
