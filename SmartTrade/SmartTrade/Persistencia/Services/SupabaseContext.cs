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
    public partial class SupabaseContext : DbContext
    {
        private static readonly string ConnectionString = "User Id=postgres;Password=6QgwpfPsBqcFfqHq;Host=db.apjeqdhvkthosokvpvma.supabase.co;Port=5432;Database=postgres";
        private readonly DbContextOptionsBuilder optionsBuilder;
        private static readonly SupabaseContext instance = new SupabaseContext();

        static SupabaseContext() { }
        private SupabaseContext()
        {
            
            optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(ConnectionString);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }

        public static SupabaseContext Instance
        {
            get
            {
                return instance;
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
   

    public class DBConn
    {
        public const string WebApyAuthentication = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFwamVxZGh2a3Rob3Nva3Zwdm1hIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDkyODg4NjIsImV4cCI6MjAyNDg2NDg2Mn0.f7CoRtVVO60qER7HzCtcZyELhSEf_GbdMsSYiJq_7iE";
    }
}

