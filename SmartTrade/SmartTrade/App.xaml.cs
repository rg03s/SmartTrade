using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using SmartTrade.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade
{
    public partial class App : Application
    {

        ISTService service = STService.Instance;

        public App()
        {
            InitializeComponent();
            MostrarCatalogo();
        }

        private async void MostrarCatalogo()
        {
            STService service = STService.Instance;
            List<Producto> productos = await service.GetAllProductos();
            MainPage = new NavigationPage(new Catalogo(productos));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
