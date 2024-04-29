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
        double costeTotal = 0, puntosObtenidos = 0;

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
            Span span_costeTotal = this.FindByName<Span>("span_costeTotal");
            Span span_puntosObtenidos = this.FindByName<Span>("span_puntosObtenidos");

            //eliminar los elementos del stacklayout para evitar duplicados
            stackLayout.Children.Clear();

            foreach (ItemCarrito item in carrito)
            {
                crearTarjeta(item, stackLayout);
            }

            span_costeTotal.Text = "Coste total: " + costeTotal + "€";
            span_puntosObtenidos.Text = "Puntos obtenidos: " + puntosObtenidos;

        }

        private async void crearTarjeta(ItemCarrito item, StackLayout stackLayout)
        {
            try
            {
                Producto producto = await service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
                Producto_vendedor productoVendedor = producto.Producto_Vendedor.FirstOrDefault(pv => pv.Id == item.idProductoVendedor);

                costeTotal += productoVendedor.Precio * item.Cantidad;
                puntosObtenidos += producto.Puntos * item.Cantidad;

                // Crear botón redondeado personalizado para sumar la cantidad
                var roundedButtonPlus = new Button
                {
                    Text = "+",
                    TextColor = Color.Black,
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BackgroundColor = Color.FromHex("#f0f0f0"),
                    CornerRadius = 5,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                // Crear etiqueta para mostrar la cantidad
                Label cantidadLabel = new Label { Text = item.Cantidad.ToString(), FontAttributes = FontAttributes.Bold, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center };

                // Añadir gesto de tap para sumar la cantidad al hacer clic
                roundedButtonPlus.Clicked += (sender, args) =>
                {
                    // Sumar la cantidad al hacer clic en el botón de sumar
                    item.Cantidad++;
                    cantidadLabel.Text = item.Cantidad.ToString();
                };

                // Crear botón redondeado personalizado para restar la cantidad
                var roundedButtonMinus = new Button
                {
                    Text = "-",
                    TextColor = Color.Black,
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BackgroundColor = Color.FromHex("#f0f0f0"),
                    CornerRadius = 5,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                // Añadir gesto de tap para restar la cantidad al hacer clic
                roundedButtonMinus.Clicked += (sender, args) =>
                {
                    // Restar la cantidad al hacer clic en el botón de restar
                    if (item.Cantidad > 0)
                    {
                        item.Cantidad--;
                        cantidadLabel.Text = item.Cantidad.ToString();
                    }
                };

                // Crear el StackLayout para los botones de cantidad
                var quantityButtonsLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Children =
                    {
                        new Label { Text = "Cantidad:", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center },
                        roundedButtonMinus,
                        cantidadLabel,
                        roundedButtonPlus
                    }
                };

                // Crear la tarjeta del producto con botones redondeados y lógica de cantidad
                var productCard = new Frame
                {
                    BackgroundColor = Color.White,
                    CornerRadius = 10,
                    Margin = new Thickness(10),
                    Padding = new Thickness(10),
                    HasShadow = true,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 10,
                        Children =
                            {
                                new Image
                                {
                                    Source = producto.Imagen,
                                    HeightRequest = 100,
                                    WidthRequest = 100,
                                    Aspect = Aspect.AspectFit,
                                    GestureRecognizers =
                                    {
                                        new TapGestureRecognizer
                                        {
                                            Command = new Command(async () =>
                                            {
                                                // Navegar a la página de información del producto
                                                await Navigation.PushAsync(new ProductPage(service, producto));
                                            })
                                        }
                                    }
                                },
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 5,
                                    Children =
                                    {
                                        new Label { Text = producto.Nombre, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.Black, FontAttributes = FontAttributes.Bold },
                                        new Label { Text = producto.Descripcion, TextColor = Color.Gray },
                                        new Label { Text = "Características: --", TextColor = Color.Gray },
                                        quantityButtonsLayout, // Añadir el StackLayout de los botones de cantidad
                                        new Label { Text = productoVendedor.Precio.ToString() + "€", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black }
                                    }
                                }
                            }
                    }
                };

                // Agregar la tarjeta del producto al StackLayout principal
                stackLayout.Children.Add(productCard);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el producto: {e.Message}");
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