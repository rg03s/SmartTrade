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
            // DependencyService.Register<MockDataStore>();

            MainPage = new NavigationPage(new LoginPage(service));

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
