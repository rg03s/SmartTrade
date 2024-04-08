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
        private STService service;
        public SeleccionRegistro(STService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private async void RegistroVendedor(object sender, EventArgs e)
        {
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
            LoginPage inicioSesion = new LoginPage(service);
            await Navigation.PushAsync(inicioSesion);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Usuario u = new Usuario("nataliaaa2355", "natalia1", "hola1234", "abcd", "natal@gmail.com", DateTime.Now);
            Usuario u = new Usuario("nataliagdaa", "NATALIAGarg", "abcd1234", "calle 2", "ngaaad@gmail.com", DateTime.Now);
            Console.WriteLine(u.ToString());
            service.AddUser(u);
            // _repo.AñadirUsuario(u);
            //_repo.AñadirUsuario(uu);
            //_repo.MostrarUsuarios();
        }
    }
}