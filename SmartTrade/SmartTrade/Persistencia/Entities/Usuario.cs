using System;
using System.Collections.Generic;
using System.Text;
//using Postgrest.Attributes;
//using Postgrest.Models;
using Postgrest;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities
{
    [Table("Usuario")]
    public partial class Usuario 
    {
        [Key]
        [Column("nickname")]
        public string Nickname { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("fecha_nac")]
        public DateTime Fecha_nac { get; set; }

        [Column ("isVendedor")]
        public bool IsVendedor { get; set; }

        [Column("cuenta_bancaria")]
        public string Cuenta_bancaria { get; set; }

        [NotMapped]
        public ICollection<Producto_vendedor> Productos_vendedor { get; set; }

        [Column("puntos")]
        public int Puntos { get; set; }
        [NotMapped]
        public ICollection<Producto> AlertasProductosSinStock { get; set; }
    }
}
