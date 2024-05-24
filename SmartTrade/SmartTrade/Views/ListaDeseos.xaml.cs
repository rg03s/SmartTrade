using Acr.UserDialogs;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using System;
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
    public partial class ListaDeseos : ContentPage
    {

        STService service;

        public ListaDeseos()
        {
            InitializeComponent();
            this.service = STService.Instance;

        }
        override protected void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando productos...");
            _ = CargarProductosListaDeseos();
            UserDialogs.Instance.HideLoading();
        }

        private async Task CargarProductosListaDeseos()
        {
            try
            {
                List <Producto> lista = await service.getProductosListaDeseos();
                foreach (Producto producto in lista) Console.WriteLine(producto.Nombre);
               // List<ItemCarrito> carrito = await service.GetCarrito();
                StackLayout listaProd = this.FindByName<StackLayout>("listaItems");

                listaProd.Children.Clear();

                if (lista.Count == 0)
                {

                    StackLayout stackLayoutProductoVacio = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            new Label
                            {
                                Text = "No hay productos guardados en la lista",
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                TextColor = Color.Black,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
                    };

                    listaProd.Children.Add(stackLayoutProductoVacio);

                    return;
                }
                else
                {
                   await MostrarProductosLista(lista);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar los productos de la lista: {e.Message}");
            }
        }

        private async Task MostrarProductosLista(List<Producto> lista)
        {

            StackLayout stackLayout = this.FindByName<StackLayout>("listaItems");

            foreach (Producto producto in lista)
            {
                await CrearTarjeta(producto, stackLayout);
            }

        }

private async Task CrearTarjeta(Producto producto, StackLayout stackLayout)
{
    try
    {
        string caracteristicas = "";

        Button btnCarrito = new Button
        {
            Text = "Añadir al carrito",
            HorizontalOptions = LayoutOptions.EndAndExpand,
            BackgroundColor = Color.FromHex("#2961AF"),
            TextColor = Color.White,
            CornerRadius = 10
        };

        Picker tallaPicker = new Picker
        {
            Title = "Talla",
            SelectedIndex = 0,
            HeightRequest = 40,
            WidthRequest = 130,
            ItemsSource = new List<string> { "XS", "S", "M", "L", "XL" }
        };

        Button btnEliminarFav = new Button
        {
            Text = "&#xf004;",
            FontFamily = Device.RuntimePlatform == Device.iOS ? "FontAwesome" : "FontAwesomeSolid",
            FontSize = 19,
            TextColor = Color.Red,
            BackgroundColor = Color.Transparent,
            WidthRequest = 45,
            HeightRequest = 40,
        };

        btnCarrito.Clicked += async (sender, e) =>
        {
            if (await service.ProductoEnGuardarMasTarde(producto)) await service.EliminarProductoGuardarMasTarde(producto);
            if (producto.Categoria == "Ropa") caracteristicas = ("Talla " + tallaPicker.SelectedItem.ToString());
            ItemCarrito item = new ItemCarrito(producto.Producto_Vendedor.First().Id, 1, service.GetUsuarioLogueado(), caracteristicas);
            await service.AgregarItemCarrito(item);
            await service.EliminarProductoListaDeseos(producto.Producto_Vendedor.First());
            await CargarProductosListaDeseos();
        };

        var frame = new Frame
        {
            CornerRadius = 10,
            Margin = new Thickness(0, 0, 0, 10),
            BackgroundColor = Color.White,
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new AbsoluteLayout
                            {
                                Children =
                                {
                                    new Image
                                    {
                                        Source = producto.Imagen,
                                        HeightRequest = 150
                                    },
                                    btnEliminarFav
                                },
                            },
                            new StackLayout
                            {
                                Orientation = StackOrientation.Vertical,
                                Margin = new Thickness(10, 0, 0, 0),
                                Children =
                                {
                                    new Label { Text = producto.Nombre, FontSize = 20, FontAttributes = FontAttributes.Bold, TextColor = Color.Black },
                                    new Label { Text = producto.Descripcion.Length > 50 ? producto.Descripcion.Substring(0, 50) + "..." : producto.Descripcion, FontSize = 16, TextColor = Color.Black },
                                    new Label {
                                        FormattedText = new FormattedString
                                        {
                                            Spans =
                                            {
                                                new Span { Text = "Vendedor: ", FontSize = 16, TextColor = Color.Black },
                                                new Span { Text = producto.Producto_Vendedor.First().NicknameVendedor, FontSize = 16, TextColor = Color.RoyalBlue, FontAttributes = FontAttributes.Bold}
                                            }
                                        }
                                    },
                                    new Label
                                    {
                                        FormattedText = new FormattedString
                                        {
                                            Spans =
                                            {
                                                new Span { Text = "Precio: ", FontSize = 16, TextColor = Color.Black },
                                                new Span { Text = producto.Producto_Vendedor.First().Precio.ToString() + "€", FontSize = 16, TextColor = Color.Black, FontAttributes = FontAttributes.Bold }
                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Children =
                                        {
                                            new Image
                                            {
                                                Source = new UriImageSource { Uri = new Uri("https://i.ibb.co/NZ99Tp4/Huella-Eco.png") },
                                                Aspect = Aspect.AspectFill,
                                                HeightRequest = 13
                                            },
                                            new Label
                                            {
                                                FormattedText = new FormattedString
                                                {
                                                    Spans =
                                                    {
                                                        new Span { Text = producto.Huella_eco, FontSize = 16, TextColor = Color.Green },
                                                        new Span { Text = " +" + producto.Puntos + " puntos", FontSize = 12 }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(0, 10, 0, 0),
                        Children =
                        {
                            tallaPicker,
                            btnCarrito
                        }
                    }
                }
            }
        };
        stackLayout.Children.Add(frame);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error al crear la tarjeta: {e.Message}");
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