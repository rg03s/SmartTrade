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

namespace SmartTrade.ViewModels
{
    internal class SupabaseClient
    {
        private string supabaseUrl;
        private string supabaseKey;

        public SupabaseClient(string supabaseUrl, string supabaseKey)
        {
            this.supabaseUrl = supabaseUrl;
            this.supabaseKey = supabaseKey;
        }
    }
}