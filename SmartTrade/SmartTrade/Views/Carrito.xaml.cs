using SmartTrade.Logica.Services;
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
    public partial class Carrito : ContentPage
    {

        ISTService service;

        public Carrito(ISTService service)
        {
            InitializeComponent();
            this.service = service;


        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            //TODO 
            Console.WriteLine("Perfil");
        }

        private void BtnFinalizarCompra_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Finalizar Compra");
        }

        private void BtnSumar_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Increase Quantity");
        }

        private void BtnRestar_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Decrease Quantity");
        }
    }
}