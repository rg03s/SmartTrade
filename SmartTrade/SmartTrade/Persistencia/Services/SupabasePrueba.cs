using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SmartTrade.Entities;
using Supabase.Gotrue;
using System.Security.Cryptography;
namespace SmartTrade.Persistencia.Services
{
    public class SupabasePrueba : DbContext
    {
        private static readonly SupabasePrueba instance = new SupabasePrueba();
        private readonly DbContextOptionsBuilder optionsBuilder;

        static SupabasePrueba() { }

        private SupabasePrueba()
        {
            optionsBuilder = new DbContextOptionsBuilder(); 
            optionsBuilder.UseNpgsql("User Id=postgres.apjeqdhvkthosokvpvma;Password=zGvvULbrYK4XeQe2;Server=aws-0-eu-west-2.pooler.supabase.com;Port=5432;Database=postgres");
        }

        public static SupabasePrueba Instance
        {
            get
            {
                return instance;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("User Id=postgres.apjeqdhvkthosokvpvma;Password=zGvvULbrYK4XeQe2;Server=aws-0-eu-west-2.pooler.supabase.com;Port=6543;Database=postgres");

        public DbSet<Entities.Usuario> Usuario { get; set; }
    }
}

