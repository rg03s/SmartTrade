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
            pedido.IdProductoVendedor = new List<int>();
            pedido.NickComprador = "rgc";
            pedido.Direccion = "si";
            pedido.Num_tarjeta = 123456789;
            pedido.Id = 0;
            pedido.IdProductoVendedor.Add(1);
            pedido.IdProductoVendedor.Add(12);
            pedido.IdProductoVendedor.Add(2);
            pedido.Puntos_obtenidos = 0;
            //pedido.Estado = "Entregado";

            ///MainPage = new LoginPage();
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
