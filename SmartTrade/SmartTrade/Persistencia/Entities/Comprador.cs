using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Supabase;
using SmartTrade.Entities;

namespace SmartTrade.Entities
{
    public partial class Comprador : Usuario
    {
       
        public int Puntos { get; set; }

    }
}
