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
using System.Diagnostics;
using System.Security.Cryptography;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Busqueda : ContentPage
    {
        private STService service;
        private string busqueda;
        private List<Producto> productosBuscados;
        public Busqueda(STService service, string busqueda, List<Producto> productosB)
        {
            InitializeComponent();
            this.service = service;
            this.productosBuscados = productosB;
            this.busqueda = busqueda;
            this.BindingContext = new BusquedaViewModel(service, busqueda, productosBuscados);
            ProductosCollectionView.ItemsSource = productosBuscados;
            OnPropertyChanged(nameof(BindingContext));
        }
    }
}