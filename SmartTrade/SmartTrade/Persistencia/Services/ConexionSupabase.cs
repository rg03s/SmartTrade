using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Supabase;
using Supabase.Functions;
using Supabase.Interfaces;
using SmartTrade;
using System.Data;
using SmartTrade.Entities;
using static System.Net.WebRequestMethods;
using Microsoft.EntityFrameworkCore;


namespace SmartTrade.Persistencia.Services
{
    public partial class ConexionSupabase : DbContext
    {
        private static readonly string ConnectionString = "User Id=postgres;Password=hXz9CorkXzkqIj9z;Host=db.apjeqdhvkthosokvpvma.supabase.co;Port=5432;Database=postgres";

        public ConexionSupabase() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Comprador> Comprador { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Producto_vendedor> Producto_Vendedor { get; set; }
        public DbSet<Valoracion> Valoracion { get; set; }



    }
}

