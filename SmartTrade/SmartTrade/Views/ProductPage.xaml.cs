using SmartTrade.Persistencia.Services;
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
    public partial class ProductPage : ContentPage
    {

        private ISTService service;

        public ProductPage(ISTService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            //TODO
            //cambiar de vista a catalogo. Pongo a login temporalmente
            LoginPage login = new LoginPage(service);
        }

        private void BtnCarrito_click(object sender, EventArgs e)
        {
            //TODO
            //cambiar de vista a carrito
            Console.WriteLine("Carrito");
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Perfil");
        }

        private void BtnAgregarCarrito_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Carrito");
        }
        private void BtnModelo3d_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Modelo 3D");
        }

        private void BtnVerComentarios_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Ver Comentarios");
        }
    }
}