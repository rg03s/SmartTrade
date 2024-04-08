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
using SmartTrade.Persistencia.Repositorios;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionRegistro : ContentPage
    {
        private ISTService service;
        private RepositorioUsuario _repo;

        public SeleccionRegistro(ISTService service)
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
            SeleccionRegistro inicioSesion = new SeleccionRegistro(service);
            await Navigation.PushAsync(inicioSesion);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SupabasePrueba sc = SupabasePrueba.Instance;
            _repo = new RepositorioUsuario(sc);
           // Usuario u = new Usuario("nataliaaa2355", "natalia1", "hola1234", "abcd", "natal@gmail.com", DateTime.Now);
            Usuario uu = new Usuario("nataliagd", "NATALIA", "abcd1234","calle 2", "ngd@gmail.com", DateTime.Now);
            Console.WriteLine(uu.ToString());
           // _repo.AñadirUsuario(u);
            _repo.AñadirUsuario(uu);
            _repo.MostrarUsuarios();
        }
    }
}