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

        private STService service;

        public ProductPage(STService service, Producto producto)
        {
            InitializeComponent();
            this.service = service;

            #region TEST
            //test
            /*
            Dictionary<string, object> atributosRopa = new Dictionary<string, object>
            {
                {"talla", "Talla S"}, 
                {"color", "blanco"},
                {"marca", "Adidas"},
                {"tipoPrenda", "Camiseta"}
            };

            Dictionary<string, object> atributosTecnologia = new Dictionary<string, object>
            {
                {"dispositivo", "Ipad"},
                {"marca", "Apple"},
                {"modelo", "Air 5"},
            };

            Dictionary<string, object> atributosDeporte = new Dictionary<string, object>
            {
                {"tipo", "tenis"}
            };

            Dictionary<string, object> atributosPapeleria = new Dictionary<string, object>
            {
                {"material", "papel"}
            };*/

            //producto de prueba. Debe ser el que se pase por el constructor
            //IFabrica fabrica = new Fabrica();
            //Vendedor vendedor = new Vendedor("test", "test", "test", "test", "test@test.com", DateTime.Now, "test");
            //Categoria categoria = new Categoria("papeleria");
            //Producto producto = fabrica.CrearProducto("Paquete de folios A3", "35%",
            // "https://m.media-amazon.com/images/I/71EQOfPQ+nL._AC_SY300_SX300_.jpg", 
            //"modelo3d", "Esto es la descripcion del paquete de folios A3", 
            //10, categoria, atributosPapeleria);
            #endregion

            //Span span_vendedor = (Span)FindByName("vendedor");
            //span_vendedor.Text = producto.Producto_Vendedor.First().nicknameVendedor;

            Image imagen_producto = (Image)FindByName("imagen_producto");
            imagen_producto.Source = producto.Imagen;

            Span huella_eco = (Span)FindByName("huella_eco");
            huella_eco.Text = producto.Huella_eco;

            Label titulo_producto = (Label)FindByName("titulo_producto");
            titulo_producto.Text = producto.Nombre;

            Label prod_desc = (Label)FindByName("producto_descripcion");
            prod_desc.Text = producto.Descripcion;

            Label precio_producto = (Label)FindByName("precio_producto");
            precio_producto.Text = producto.Producto_Vendedor.First().Precio.ToString() + "€";
            
            Picker picker = (Picker)FindByName("vendedorPicker");
            foreach (Producto_vendedor pv in producto.Producto_Vendedor)
            {
                picker.Items.Add(pv.nicknameVendedor);
            }
            picker.SelectedItem = picker.Items[0];

            //on change picker selected item change precio_producto
            picker.SelectedIndexChanged += (sender, args) =>
            {
                string selected = picker.Items[picker.SelectedIndex];
                precio_producto.Text = producto.Producto_Vendedor.Where(pv => pv.nicknameVendedor == selected).First().Precio.ToString() + "€";
            };
            
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

            } else if (producto is Tecnologia prod_tec)
            {
                Span dispositivo = (Span)FindByName("dispositivo_tec");
                dispositivo.Text = prod_tec.Dispositivo;

                Span marca = (Span)FindByName("marca_tec");
                marca.Text = "Marca: " + prod_tec.Marca;

                Span modelo = (Span)FindByName("modelo_tec");
                modelo.Text = "Modelo: " + prod_tec.Modelo;

                Label label_dispositivo_tec = (Label)FindByName("label_dispositivo_tec");
                Label label_marca_tec = (Label)FindByName("label_marca_tec");
                Label label_modelo_tec = (Label)FindByName("label_modelo_tec");

                label_dispositivo_tec.IsVisible = true;
                label_marca_tec.IsVisible = true;
                label_modelo_tec.IsVisible = true;

            } else if (producto is Deporte prod_dep)
            {
                Span tipo = (Span)FindByName("tipo_dep");
                tipo.Text = "Tipo: " + prod_dep.Tipo;

                Label label_tipo_dep = (Label)FindByName("label_tipo_dep");
                label_tipo_dep.IsVisible = true;

            }
            else if (producto is Papeleria prod_papeleria)
            {
                Span material_papeleria = (Span)FindByName("material_papeleria");
                material_papeleria.Text = "Tipo: " + prod_papeleria.Material;

                Label label_material_papeleria = (Label)FindByName("label_material_papeleria");
                label_material_papeleria.IsVisible = true;
            }
        }

       

        public List<Producto> ObtenerPrimerProducto()
        {
            List<Producto> pr = null;

            // Declarar una tarea para obtener los productos de forma asíncrona
            Task<List<Producto>> tarea = service.GetAllProductsAsync();

            // Esperar a que la tarea se complete de forma síncrona
            tarea.Wait();

            // Obtener el resultado de la tarea
            pr = tarea.Result;

            return pr;
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