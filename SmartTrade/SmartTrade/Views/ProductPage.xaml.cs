using SmartTrade.Entities;
using SmartTrade.Logica.Entities;
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

            //test
            Dictionary<string, object> atributosDeporte = new Dictionary<string, object>
            {
                {"tipo", "Fútbol"}
            };

            IFabrica fabrica = new Fabrica();
            Vendedor vendedor = new Vendedor("test", "test", "test", "test", "test@test.com", DateTime.Now, "test");
            Categoria categoria = new Categoria("Ropa");
            Producto producto = fabrica.CrearProducto("deporte", "Balón de fútbol", "balon", "balon.jpg", "balon3d", "Balón de fútbol", 10, vendedor, categoria, 10, 10, atributosDeporte);

            Console.WriteLine(producto.Nombre);
            //TODO
            //cambiar informacion por la del producto que se tiene que recibir en el constructor
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