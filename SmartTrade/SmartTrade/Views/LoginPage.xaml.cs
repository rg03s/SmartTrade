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
        private ISTService service;
        //private string username;
        //private string password;
        private ProductPage productPage;
        //private Registro registro;
        //private LoginPage loginPage;

        public LoginPage(ISTService service)
        {
            this.service = service;
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void Ingresar_Clicked (object sender, EventArgs e)
        {
            try
            {
                string username = correo.Text;
                string password = contraseña.Text;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Error", "Por favor, ingrese su correo y contraseña", "OK");
                }
                else
                {
                    if (await service.Login(username, password))
                    {
                        correo.Text = string.Empty;
                        contraseña.Text = string.Empty;
                        productPage = new ProductPage(service);
                        await Navigation.PushAsync(productPage);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Correo o contraseña incorrectos", "OK");
                    }
                }
            }  
            catch (Exception)
            {
                correo.Text = string.Empty;
                contraseña.Text = string.Empty;
            }
        }
        private void VerContraseña_Changed(object sender, TextChangedEventArgs e)
        {
            if (VerContraseña.IsChecked == true) contraseña.IsPassword = false;
            else contraseña.IsPassword = true; ;
        }
    }
}