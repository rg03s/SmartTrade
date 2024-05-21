using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    [Table ("GuardarMasTarde")]
    public partial class GuardarMasTardeItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("propietario")]
        public string NickPropietario { get; set; }
        [Column("productoId")]
        public int ProductoId { get; set; }
    }
}
