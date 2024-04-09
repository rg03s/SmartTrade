using System;
using System.Collections.Generic;
using System.Text;
using Postgrest.Attributes;

namespace SmartTrade.Entities
{
    [Table("Categoria")]
    public partial class Categoria
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

    }
}
