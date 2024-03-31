using SmartTrade.Persistencia.Services;
using SmartTrade.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade
{
    public partial class App : Application
    {

        public App()
        {
            string url = "https://apjeqdhvkthosokvpvma.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFwamVxZGh2a3Rob3Nva3Zwdm1hIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDkyODg4NjIsImV4cCI6MjAyNDg2NDg2Mn0.f7CoRtVVO60qER7HzCtcZyELhSEf_GbdMsSYiJq_7iE";

            InitializeComponent();
            STService service = new STService(new STDAL(new ConexionSupabase()));
            DependencyService.Register<MockDataStore>();
            MainPage = new Registro(service);
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
