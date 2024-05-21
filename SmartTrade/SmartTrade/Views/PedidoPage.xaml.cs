using Acr.UserDialogs;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

//using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

//using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoPage : ContentPage
    {

        STService service;
        List<ItemCarrito> Carrito;
        public Producto_vendedor productoVendedor_seleccionado;

        public PedidoPage(List <ItemCarrito> carrito)
        {
            InitializeComponent();
            this.service = STService.Instance;
            this.Carrito = carrito;
            Console.WriteLine("holaaa" + carrito.Count);
        } 
        override protected void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando...");
            CargarProductosLista();
            UserDialogs.Instance.HideLoading();
        }

        private async Task CargarProductosLista()
        {
            try
            {
                StackLayout listaProd = this.FindByName<StackLayout>("listaItems");

                listaProd.Children.Clear();
                await MostrarProductos(Carrito);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar los productos de la lista: {e.Message}");
            }
        }

        private async Task MostrarProductos(List<ItemCarrito> carrito)
        {

            StackLayout stackLayout = this.FindByName<StackLayout>("listaItems");

            foreach (ItemCarrito item in Carrito)
            {
                Console.WriteLine("EL ITEM " + item.Id);
                await crearTarjeta(item, stackLayout);
            }

        }

        private async Task crearTarjeta(ItemCarrito item, StackLayout stackLayout)
        {
            try
            {
                int cantidad = item.Cantidad;
                //De product_vendedor saco el precio 
                Producto_vendedor product_vendedor =  await service.GetProductoVendedorById(item.idProductoVendedor);
                //De producto saco los datos de él, nombre, imagen ...
                int idProducto = product_vendedor.IdProducto;
                Producto producto = await service.GetProductoById(idProducto);
                Console.WriteLine("PRODUCCCTO:" + producto.Id);
                string caracteristicas = "";
                double precio_total = cantidad * product_vendedor.Precio;

 
                var productCard = new Frame
                {
                    BackgroundColor = Color.White,
                    CornerRadius = 10,
                    Margin = new Thickness(10),
                    Padding = new Thickness(10),
                    HasShadow = true,
                    Content = new Grid
                    {
                        Padding = 15,
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }
                        }
                    }
                };
                var grid = (Grid)productCard.Content;
                
                grid.Children.Add(
                                    new Image
                                    {
                                        Source = producto.Imagen,
                                        HeightRequest = 80,
                                        WidthRequest = 80,
                                        Aspect = Aspect.AspectFit,
                                    },
                                   0, 1
                                );
                grid.Children.Add(
                                new Label
                                {
                                    Text = producto.Nombre,
                                    FontAttributes = FontAttributes.Bold
                                },
                               1, 1
                            );
                grid.Children.Add(
                                new Label
                                {
                                    Text ="Cantidad: " + cantidad,
                                    FontAttributes = FontAttributes.Italic,
                                    VerticalOptions = LayoutOptions.End,
                                    MaxLines = 2,
                                    LineBreakMode = LineBreakMode.TailTruncation
                                },
                               1, 2
                            );
                grid.Children.Add(
                               new Label
                               {
                                   Text = "Total: " + precio_total  + "€",
                                   FontAttributes = FontAttributes.Italic,
                                   VerticalOptions = LayoutOptions.End,
                                   MaxLines = 2,
                                   LineBreakMode = LineBreakMode.TailTruncation
                               },
                              2, 2
                           );
                stackLayout.Children.Add(productCard);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el productoO: {e.Message}");
            }
        }


        private void BtnAtras_click(object sender, EventArgs e)
        {
            Console.WriteLine("Atras");
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

    }
}