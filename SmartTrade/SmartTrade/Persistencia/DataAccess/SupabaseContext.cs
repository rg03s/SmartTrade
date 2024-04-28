using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SmartTrade.Entities;
using System.Security.Cryptography;

namespace SmartTrade.Persistencia.DataAccess
{
    public class SupabaseContext : DbContext
    {
        private static readonly SupabaseContext instance = new SupabaseContext();
        private readonly DbContextOptionsBuilder optionsBuilder;

        static SupabaseContext() { }

        private SupabaseContext()
        {
            optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql("User Id=postgres.apjeqdhvkthosokvpvma;Password=zGvvULbrYK4XeQe2;Server=aws-0-eu-west-2.pooler.supabase.com;Port=5432;Database=postgres");
        }

        public static SupabaseContext Instance
        {
            get
            {
                return instance;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("User Id=postgres.apjeqdhvkthosokvpvma;Password=zGvvULbrYK4XeQe2;Server=aws-0-eu-west-2.pooler.supabase.com;Port=5432;Database=postgres");

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarjeta> Tarjeta { get; set; }
        //  public DbSet<Entities.Vendedor> Vendedor { get; set; }
        //        

        // public DbSet<Entities.Comprador> Comprador { get; set; }
        public DbSet<Producto_vendedor> Producto_Vendedor { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Deporte> Deporte { get; set; }
        public DbSet<Papeleria> Papeleria { get; set; }
        public DbSet<Ropa> Ropa { get; set; }
        public DbSet<Tecnologia> Tecnologia { get; set; }
        public DbSet<ItemCarrito> Carrito { get; set; }
        /*
       
        public DbSet<Pedido> Pedido { get; set; }
        
        
        public DbSet<Valoracion> Valoracion { get; set; }
       
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasDiscriminator(p => p.Categoria)
                .HasValue<Papeleria>("Papeleria")
                .HasValue<Ropa>("Ropa")
                .HasValue<Tecnologia>("Tecnologia")
                .HasValue<Deporte>("Deporte");

        }

    }
}



