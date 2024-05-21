using Acr.UserDialogs;
using SmartTrade.Entities;
using SmartTrade.Logica.Entities;
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
    public partial class ProductPage : ContentPage
    {

        private STService service;
        private Producto_vendedor productoVendedor_seleccionado;
        private string tallaSeleccionada;
        private const string corazonVacioUrl = "https://i.ibb.co/MfL1wHp/corazon-vacio.png";
        private const string corazonLlenoUrl = "https://i.ibb.co/Pzq5CQT/corazon-lleno.png";
        Producto productoVista;
        public ProductPage(Producto producto)
        {
            InitializeComponent();
            this.service = STService.Instance;
            productoVista = producto;
            Image imagen_producto = (Image)FindByName("imagen_producto");
            imagen_producto.Source = producto.Imagen;

            Span huella_eco = (Span)FindByName("huella_eco");
            huella_eco.Text = producto.Huella_eco;

            Label titulo_producto = (Label)FindByName("titulo_producto");
            titulo_producto.Text = producto.Nombre;

            Label prod_desc = (Label)FindByName("producto_descripcion");
            prod_desc.Text = producto.Descripcion;

            Label precio_producto = (Label)FindByName("precio_producto");
            precio_producto.Text = producto.Producto_Vendedor.OrderBy(pv => pv.Precio).First().Precio.ToString() + "€";
            
            Picker picker = (Picker)FindByName("vendedorPicker");
            foreach (Producto_vendedor pv in producto.Producto_Vendedor)
            {
                picker.Items.Add(pv.NicknameVendedor + " - " + pv.Precio + "€");
            }
            picker.SelectedItem = producto.Producto_Vendedor.OrderBy(pv => pv.Precio).First().NicknameVendedor + " - " + producto.Producto_Vendedor.OrderBy(pv => pv.Precio).First().Precio + "€";
            productoVendedor_seleccionado = producto.Producto_Vendedor.OrderBy(pv => pv.Precio).First();

            picker.SelectedIndexChanged += (sender, args) =>
            {
                string selected = picker.Items[picker.SelectedIndex];
                selected = selected.Split('-')[0].Trim();
                productoVendedor_seleccionado = producto.Producto_Vendedor.Where(pv => pv.NicknameVendedor == selected).First();
                precio_producto.Text = productoVendedor_seleccionado.Precio.ToString() + "€";
            };
            
            if(producto is Ropa prod_ropa)
            {

                //Span talla_ropa = (Span)FindByName("talla_ropa");
                //talla_ropa.Text = prod_ropa.Talla;

                configurarPicker();

                Span color_ropa = (Span)FindByName("color_ropa");
                color_ropa.Text = prod_ropa.Color;

                Span marca_ropa = (Span)FindByName("marca_ropa");
                marca_ropa.Text = prod_ropa.Marca;

                Span tipoPrenda_ropa = (Span)FindByName("tipoPrenda_ropa");
                tipoPrenda_ropa.Text = prod_ropa.TipoPrenda;

                Label label_marca_ropa = (Label)FindByName("label_marca_ropa");
                //Label label_talla_ropa = (Label)FindByName("label_talla_ropa");
                Label label_color_ropa = (Label)FindByName("label_color_ropa");
                Label label_tipoPrenda_ropa = (Label)FindByName("label_tipoPrenda_ropa");

                //label_talla_ropa.IsVisible = true;
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

            configurarBotones();
            configurarBotonGuardarMasTarde();
        }

        private async Task configurarBotones()
        {
            bool estaEnLista = await service.ProductoEnListaDeseos(service.GetUsuarioLogueado(), productoVendedor_seleccionado);
            if (!estaEnLista) btnDeseos.Source = corazonVacioUrl;
            else btnDeseos.Source = corazonLlenoUrl;

        }
        private async Task configurarBotonGuardarMasTarde()
        {
            if (await service.ProductoEnGuardarMasTarde(productoVista)) GuardarMasTardeButton.TextColor = Color.DeepSkyBlue;
            else GuardarMasTardeButton.TextColor = Color.White;
        }
        
        private void configurarPicker()
        {
            Picker picker = (Picker)FindByName("tallaPicker");
            picker.IsVisible = true;
            picker.Items.Add("Talla XS");
            picker.Items.Add("Talla S");
            picker.Items.Add("Talla M");
            picker.Items.Add("Talla L");
            picker.Items.Add("Talla XL");
            picker.Items.Add("Talla XXL");
            picker.SelectedIndex = 0;
            tallaSeleccionada = picker.Items[picker.SelectedIndex];
            picker.SelectedIndexChanged += (sender, args) =>
            {
                tallaSeleccionada = picker.Items[picker.SelectedIndex];          
            };
        }
       
        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnCarrito_click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carrito());
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Perfil");
        }
        private async void BtnAgregarGuardarMasTarde_clickAsync(object sender, EventArgs e)
        {
            bool estaEnLista = await service.ProductoEnGuardarMasTarde(productoVista);
            if (estaEnLista)
            {
                GuardarMasTardeButton.TextColor = Color.White;
                await service.EliminarProductoGuardarMasTarde(productoVista);
                
                UserDialogs.Instance.Toast("Producto eliminado de guardar para más tarde", TimeSpan.FromSeconds(3));
            } 
            if (!estaEnLista)
            {
                GuardarMasTardeButton.TextColor = Color.DeepSkyBlue;
                await service.AgregarProductoGuardarMasTarde(productoVista);
               
                UserDialogs.Instance.Toast("Producto guardado para más tarde", TimeSpan.FromSeconds(3));
            }
        }
        
        private async void BtnAgregarCarrito_clickAsync(object sender, EventArgs e)
        {
            if (await service.ProductoEnGuardarMasTarde(productoVista)) await service.EliminarProductoGuardarMasTarde(productoVista);
            ItemCarrito item = new ItemCarrito(productoVendedor_seleccionado.Id, 1, service.GetUsuarioLogueado(), tallaSeleccionada);
            var confirmacion = await DisplayAlert("Confirmación", "¿Desea agregar este producto al carrito?", "SI", "NO");
            if (confirmacion) {
                if (await service.AgregarItemCarrito(item))
                {
                    UserDialogs.Instance.Toast("Producto añadido al carrito", TimeSpan.FromSeconds(3));
                }
            }
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


        private async void BtnDeseos_ClickedAsync(object sender, EventArgs e)
        {
            bool estaEnLista = await service.ProductoEnListaDeseos(service.GetUsuarioLogueado(), productoVendedor_seleccionado);
            if (!estaEnLista) 
            {
                await service.AgregarProductoListaDeseos(productoVendedor_seleccionado);
                btnDeseos.Source = corazonLlenoUrl;
            }
            if (estaEnLista) 
            {
                await service.EliminarProductoListaDeseos(productoVendedor_seleccionado);
                btnDeseos.Source = corazonVacioUrl;
            }
        }
    }
}