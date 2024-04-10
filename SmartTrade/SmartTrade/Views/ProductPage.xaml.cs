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
                {"talla", "Talla S"}, 
                {"color", "blanco"},
                {"marca", "Adidas"},
                {"tipoPrenda", "Camiseta"}
            };

            //producto de prueba. Debe ser el que se pase por el constructor
            IFabrica fabrica = new Fabrica();
            Vendedor vendedor = new Vendedor("test", "test", "test", "test", "test@test.com", DateTime.Now, "test");
            Categoria categoria = new Categoria("ropa");
            Producto producto = fabrica.CrearProducto("Camiseta VCF titulo", "10%", 
                "https://images.footballfanatics.com/valencia-cf/valencia-puma-home-shirt-2023-24_ss5_p-13384602+pv-2+u-am5bcywj7qnu89ui9y5w+v-svw30hfxj2yzagdknjx7.jpg?_hv=2&w=900", 
                "modelo3d", "Esto es la descripcion de la camiseta VCF", 
                10, vendedor, categoria, 10, 69.99, atributosDeporte);

            //TODO
            //cambiar informacion por la del producto que se tiene que recibir en el constructor
            Span span_vendedor = (Span)FindByName("vendedor");
            //span_vendedor.Text = producto.Producto_vendedores.First().Vendedor.Nombre; no funciona
            span_vendedor.Text = "UPVShop";

            Image imagen_producto = (Image)FindByName("imagen_producto");
            imagen_producto.Source = producto.Imagen;

            Span huella_eco = (Span)FindByName("huella_eco");
            huella_eco.Text = producto.Huella_eco;

            Label titulo_producto = (Label)FindByName("titulo_producto");
            titulo_producto.Text = producto.Nombre;

            Label prod_desc = (Label)FindByName("producto_descripcion");
            prod_desc.Text = producto.Descripcion;

            Label precio_producto = (Label)FindByName("precio_producto");
            //precio_producto.Text = producto.Producto_vendedores.First().Precio.ToString(); no funciona
            precio_producto.Text = "69.99" + " €";
            
            if(producto is Ropa prod_ropa)
            {

                Span talla_ropa = (Span)FindByName("talla_ropa");
                talla_ropa.Text = prod_ropa.Talla;

                Span color_ropa = (Span)FindByName("color_ropa");
                color_ropa.Text = prod_ropa.Color;

                Span marca_ropa = (Span)FindByName("marca_ropa");
                marca_ropa.Text = prod_ropa.Marca;

                Span tipoPrenda_ropa = (Span)FindByName("tipoPrenda_ropa");
                tipoPrenda_ropa.Text = prod_ropa.TipoPrenda;

                Label label_marca_ropa = (Label)FindByName("label_marca_ropa");
                Label label_talla_ropa = (Label)FindByName("label_talla_ropa");
                Label label_color_ropa = (Label)FindByName("label_color_ropa");
                Label label_tipoPrenda_ropa = (Label)FindByName("label_tipoPrenda_ropa");

                label_talla_ropa.IsVisible = true;
                label_color_ropa.IsVisible = true;
                label_marca_ropa.IsVisible = true;
                label_tipoPrenda_ropa.IsVisible = true;

            } else if (producto is Papeleria)
            {
                //TODO
                //agregar en el xaml los label correspondientes, setear el texto y hacerlo visible. Como el caso anterior
            } else if (producto is Deporte)
            {
                //TODO
            } else if (producto is Tecnologia)
            {
                //TODO
            }



        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            //TODO
            //cambiar de vista a catalogo. Pongo a login temporalmente
            Console.WriteLine("Atras");

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