using SmartTrade.Entities;
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
        override protected void OnAppearing()
        {
            base.OnAppearing();
            CargarProductosCarrito();
        }

        private async void CargarProductosCarrito()
        {
            try
            {
                List<ItemCarrito> carrito = await service.GetCarrito();
                if (carrito.Count == 0)
                {
                    Console.WriteLine("No se han encontrado productos en el carrito");
                }
                else
                {
                    MostrarProductosCarrito(carrito);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar los productos del carrito: {e.Message}");
            }
        }

        private void MostrarProductosCarrito(List<ItemCarrito> carrito)
        {

            StackLayout stackLayout = this.FindByName<StackLayout>("listaItems");

            foreach (ItemCarrito item in carrito)
            {
                Producto producto = service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
                Producto_vendedor productoVendedor = producto.Producto_Vendedor.Where(pv => pv.Id == item.idProductoVendedor).First();

                // Crear el Frame del producto
                var productFrame = new Frame
                {
                    OutlineColor = Color.LightGray,
                    Padding = new Thickness(10),
                    CornerRadius = 10,
                    Content = new Grid
                    {
                        ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                        Children =
                {
                    new Image { Source = producto.Imagen, HeightRequest = 100, WidthRequest = 100 },
                    new StackLayout
                    {
                        Spacing = 15,
                        Children =
                        {
                            new Label { Text = producto.Nombre, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.Black },
                            new Label { Text = producto.Descripcion },
                            new Label { Text = "Características: --" },
                            new Grid
                            {
                                ColumnDefinitions = new ColumnDefinitionCollection
                                {
                                    new ColumnDefinition { Width = GridLength.Auto },
                                    new ColumnDefinition { Width = GridLength.Auto },
                                    new ColumnDefinition { Width = GridLength.Auto },
                                    new ColumnDefinition { Width = GridLength.Auto }
                                },
                                Children =
                                {
                                    new Label { Text = "Cantidad:", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                                    new Frame
                                    {
                                        WidthRequest = 20,
                                        HeightRequest = 20,
                                        CornerRadius = 5,
                                        BackgroundColor = Color.FromHex("#f0f0f0"),
                                        Padding = 0,
                                        Content = new Label { Text = "-", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                                    },
                                    new Label { Text = item.Cantidad.ToString(), FontAttributes = FontAttributes.Bold, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                                    new Frame
                                    {
                                        WidthRequest = 20,
                                        HeightRequest = 20,
                                        CornerRadius = 5,
                                        BackgroundColor = Color.FromHex("#f0f0f0"),
                                        Padding = 0,
                                        Content = new Label { Text = "+", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                                    }
                                }
                            },
                            new Label { Text = productoVendedor.Precio.ToString() + "€", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black }
                        }
                    }
                }
                    }
                };

                // Agregar el Frame al StackLayout
                stackLayout.Children.Add(productFrame);
            }

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