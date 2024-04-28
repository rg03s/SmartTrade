using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartTrade.Entities
{
    [Table("Carrito")]
    public partial class ItemCarrito
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("id_Producto_vendedor")]
        public int idProductoVendedor { get; set; }
        [Required]
        [Column("usuario")]
        public string NicknameUsuario { get; set; }
        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
