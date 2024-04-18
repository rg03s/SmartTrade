using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionRegistro : ContentPage
    {
        private STService service;
        public SeleccionRegistro(STService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private async void RegistroVendedor(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            RegistroVendedor registroVendedor = new RegistroVendedor(service);
            await Navigation.PushAsync(registroVendedor);
        }

        private async void RegistroComprador(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            Registro registroComprador = new Registro(service);
            await Navigation.PushAsync(registroComprador);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           // await Navigation.PopAsync();
            LoginPage inicioSesion = new LoginPage(service);
            await Navigation.PushAsync(inicioSesion);
        }

       
    }
}