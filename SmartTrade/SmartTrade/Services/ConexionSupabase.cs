using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Supabase;
using Supabase.Functions;
using Supabase.Interfaces;
using SmartTrade.Models;

namespace SmartTrade.Services
{
    class ConexionSupabase
    {

       
            public ConexionSupabase()
        {
            InitializeSupabase().Wait(); // Wait() bloqueará el hilo hasta que la inicialización esté completa
        }

        private async Task InitializeSupabase()
        {
            var url = Environment.GetEnvironmentVariable("https://apjeqdhvkthosokvpvma.supabase.co");
            var key = Environment.GetEnvironmentVariable("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFwamVxZGh2a3Rob3Nva3Zwdm1hIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDkyODg4NjIsImV4cCI6MjAyNDg2NDg2Mn0.f7CoRtVVO60qER7HzCtcZyELhSEf_GbdMsSYiJq_7iE");

            var options = new Supabase.SupabaseOptions
           
            {
                AutoConnectRealtime = true
            };

           var supabase = new Supabase.Client(url, key, options);
           var response = await supabase.InitializeAsync();
            
           
        }
}
}
