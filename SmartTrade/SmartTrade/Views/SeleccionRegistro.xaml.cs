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
        public SeleccionRegistro()
        {
            InitializeComponent();
            this.service = STService.Instance;
        }

        private  void RegistroVendedor(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            RegistroVendedor registroVendedor = new RegistroVendedor();
            Navigation.PushAsync(registroVendedor);
        }
        private void RegistroComprador(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            Registro registroComprador = new Registro();
            Navigation.PushAsync(registroComprador);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           // await Navigation.PopAsync();
            LoginPage inicioSesion = new LoginPage();
            Navigation.PushAsync(inicioSesion);
        }

       
    }
}