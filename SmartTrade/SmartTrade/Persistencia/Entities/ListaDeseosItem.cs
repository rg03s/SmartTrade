using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    [Table("ListaDeseos")]
    public partial class ListaDeseosItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("propietario")]
        public string NickPropietario { get; set; }
        [Column("productoVendedorId")]
        public int ProductoVendedorId { get; set; }
    }
}
