using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using SmartTrade.Persistencia.DataAccess;
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
        Pedido pedido;

        public App()
        {
            InitializeComponent();
            pedido = new Pedido();
            pedido.Fecha = DateTime.Now;
            pedido.Precio_total = 100;
            pedido.ItemsCarrito = new List<int>();
            pedido.NickComprador = "rgc";
            pedido.Direccion = "si";
            pedido.Num_tarjeta = 123456789;
            pedido.Id = 0;
            pedido.ItemsCarrito.Add(1);
            pedido.ItemsCarrito.Add(5);
            pedido.ItemsCarrito.Add(6);
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
