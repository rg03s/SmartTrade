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
            Console.WriteLine("Atras");
        }

        private void BtnCarrito_click(object sender, EventArgs e)
        {
            Console.WriteLine("Carrito");
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            Console.WriteLine("Perfil");
        }

        private void BtnAgregarCarrito_click(object sender, EventArgs e)
        {
            Console.WriteLine("Carrito");
        }
        private void BtnModelo3d_click(object sender, EventArgs e)
        {
            Console.WriteLine("Modelo 3D");
        }

        private void BtnVerComentarios_click(object sender, EventArgs e)
        {
            Console.WriteLine("Ver Comentarios");
        }
    }
}