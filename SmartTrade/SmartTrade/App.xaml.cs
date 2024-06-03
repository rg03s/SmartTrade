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

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Catalogo());
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
