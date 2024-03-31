using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Supabase;
using Supabase.Functions;
using Supabase.Interfaces;
using SmartTrade;
using System.Data;
using System.Data.Entity;
using SmartTrade.Entities;


namespace SmartTrade.Persistencia.Services
{
    class ConexionSupabase : DbContext
    {


        public ConexionSupabase()
        {
            InitializeSupabase().Wait(); // Wait() bloqueará el hilo hasta que la inicialización esté completa
        }

        private async Task InitializeSupabase()
        {
            var url = Environment.GetEnvironmentVariable("https://apjeqdhvkthosokvpvma.supabase.co");
            var key = Environment.GetEnvironmentVariable("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFwamVxZGh2a3Rob3Nva3Zwdm1hIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDkyODg4NjIsImV4cCI6MjAyNDg2NDg2Mn0.f7CoRtVVO60qER7HzCtcZyELhSEf_GbdMsSYiJq_7iE");

            var options = new SupabaseOptions

            {
                AutoConnectRealtime = true
            };

            var client = new Supabase.Client(url, key, options);
            await client.InitializeAsync();



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

