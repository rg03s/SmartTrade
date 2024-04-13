using SmartTrade.Persistencia.Services;
using SmartTrade.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;
using Supabase;
using SmartTrade.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalogo : ContentPage
    {
        private STService service;
        public Catalogo(STService service)
        {
            InitializeComponent();
            this.service = service;
            this.BindingContext = new CatalogoViewModel(service);
        }
    }
}