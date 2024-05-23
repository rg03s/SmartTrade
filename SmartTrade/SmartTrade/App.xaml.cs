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

        public App()
        {

            InitializeComponent();
            STService service = STService.Instance;
            Pedido pedido = new Pedido();
            pedido.Id = 1;
            pedido.Fecha = DateTime.Now;
            pedido.Direccion = "Calle falsa 123";
            pedido.Precio_total = 1000;
            pedido.ItemsCarrito = new List<int>();
            pedido.ItemsCarrito.Add(1);
            pedido.ItemsCarrito.Add(2);
            pedido.ItemsCarrito.Add(3);
            pedido.ItemsCarrito.Add(4);
            pedido.Num_tarjeta = 123456789;
            pedido.Puntos_obtenidos = 100;
            pedido.NickComprador = "user";
            // DependencyService.Register<MockDataStore>();

            MainPage = new NavigationPage(new MisPedidos(pedido));
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
