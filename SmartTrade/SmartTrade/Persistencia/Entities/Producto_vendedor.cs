using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Producto_vendedor
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("precio")]
        public double Precio { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("vendedor")]
        public Vendedor Vendedor { get; set; }

        [Column("producto")]
        public Producto Producto { get; set; }
    }
}
