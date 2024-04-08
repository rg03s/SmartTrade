using SmartTrade.Entities;
using SmartTrade.Persistencia.Services;
using SmartTrade.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage(STService service)
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(Navigation);
        }

        private async void Ingresar_Clicked (object sender, EventArgs e)
        {
            string username = correo.Text;
            string password = contraseña.Text;

            if (IsValidDate(username, password))
            {
                await DisplayAlert("Logrado","Inicio exitoso","OK");
                STService service = new STService(new STDAL(SupabaseContext.Instance));
                await Navigation.PushAsync(new ProductPage(service));
            }
            else
            {
                await DisplayAlert("Error", "Datos Incorrectos", "OK");
            }
        }

        private bool IsValidDate(string username, string password) 
        {
            return username == "1" && password == "1234";
        }
    }
}