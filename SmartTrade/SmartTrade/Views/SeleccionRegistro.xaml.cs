using SmartTrade.Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartTrade.Entities;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionRegistro : ContentPage
    {
        private ISTService service;
        public SeleccionRegistro(ISTService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private async void RegistroVendedor(object sender, EventArgs e)
        {
            Usuario u = new Usuario("nat", "natalia", "hola1234", "abcd", "nat@gmail.com", DateTime.Now);
            service.AddUser(u);
                await Navigation.PopAsync();
            RegistroVendedor registroVendedor = new RegistroVendedor(service);
            await Navigation.PushAsync(registroVendedor);
        }

        private async void RegistroComprador(object sender, EventArgs e) 
        {
            await Navigation.PopAsync();
            Registro registroComprador = new Registro(service);
            await Navigation.PushAsync(registroComprador);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            SeleccionRegistro inicioSesion = new SeleccionRegistro(service);
            await Navigation.PushAsync(inicioSesion);
        }
    }
}