using Acr.UserDialogs;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDeseos : ContentPage
    {

        ISTService service;

        public ListaDeseos(ISTService service)
        {
            InitializeComponent();
            this.service = service;

        }
        override protected void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando productos...");
            CargarProductosListaDeseos();
            UserDialogs.Instance.HideLoading();
        }

        private async Task CargarProductosListaDeseos()
        {
            try
            {
                string nick =  service.GetLoggedNickname();
                List <Producto> lista = await service.getProductosListaDeseos(nick);
                Console.WriteLine("Conteido lista: ");
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
                Console.WriteLine($"Error al cargar los productos del carrito: {e.Message}");
            }
        }

        private async Task MostrarProductosLista(List<Producto> lista)
        {

            StackLayout stackLayout = this.FindByName<StackLayout>("listaItems");

            foreach (Producto producto in lista)
            {
                await crearTarjeta(producto, stackLayout);
            }

        }

        private async Task crearTarjeta(Producto producto, StackLayout stackLayout)
        {
            try
            {

                Label puntos = new Label { Text = producto.Puntos.ToString(), FontAttributes = FontAttributes.Bold, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center };

                List <Producto_vendedor> pro_vendedores = producto.Producto_Vendedor.ToList();
                List <string> vendedoresDatos = new List<string>();
                string[] talla = new string[] {"XS", "S", "M", "L","XL" };

                foreach (Producto_vendedor p_vendedor in pro_vendedores) 
                {
                  //  double precio =  vendedores.Where(v => v.IdProducto == producto.Id && v.NicknameVendedor == vendedor.NicknameVendedor).Select( v => v.Precio).FirstOrDefault();
                    vendedoresDatos.Add(p_vendedor.NicknameVendedor);
                }

                string caracteristicas = "";
                

                Picker vendedorPicker = new Picker
                {
                    Title = "Elige vendedor",
                    ItemsSource = vendedoresDatos
                };
                Picker tallaPicker = new Picker
                {

                    Title = "Talla",
                    ItemsSource = talla

                };

                string precioVendedor = (vendedorPicker.SelectedItem != null) ? vendedorPicker.SelectedItem.ToString() : "No seleccionado";

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

                                    new ImageButton
                                    {
                                        Source = "https://i.ibb.co/Pzq5CQT/corazon-lleno.png",
                                        HeightRequest = 25,
                                        WidthRequest = 25,
                                        Aspect = Aspect.AspectFit,
                                        BackgroundColor = Color.White,
                                        GestureRecognizers =
                                        {
                                            new TapGestureRecognizer
                                            {
                                                Command = new Command(async () =>
                                                {
                                                    await service.EliminarProductoListaDeseos(producto);
                                                    await CargarProductosListaDeseos();
                                                })
                                            }
                                        }

                                    },
                                    0,0
                                    );
                grid.Children.Add(

                                   new Label
                                   {
                                       Text = "Puntos",
                                       FontAttributes = FontAttributes.Bold,
                                       FontSize = 16,
                                       TextColor = Color.Gray

                                   },
                                   2, 0
                                   );
                grid.Children.Add(
                                    new Image
                                    {
                                        Source = producto.Imagen,
                                        HeightRequest = 80,
                                        WidthRequest = 80,
                                        Aspect = Aspect.AspectFit,
                                        GestureRecognizers =
                                        {
                                                        new TapGestureRecognizer
                                                        {
                                                            Command = new Command(async () =>
                                                            {
                                                                await Navigation.PushAsync(new ProductPage(service, producto));
                                                            })
                                                        }
                                        }
                                    },
                                   0 , 1
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
                                    Text = producto.Descripcion,
                                    FontAttributes = FontAttributes.Italic,
                                    VerticalOptions = LayoutOptions.End,
                                    MaxLines = 2,
                                    LineBreakMode = LineBreakMode.TailTruncation
                                },
                               1, 2
                            );
                grid.Children.Add(
                                vendedorPicker,
                               2, 3
                            );
                grid.Children.Add(
                               tallaPicker,
                               1, 3
                            );
                grid.Children.Add(
                                new Button
                                {
                                    Text = "Añadir al carrito",
                                    FontAttributes = FontAttributes.Italic,
                                    VerticalOptions = LayoutOptions.End,
                                    BackgroundColor = Color.HotPink,
                                    CornerRadius = 12,
                                     GestureRecognizers =
                                        {
                                                        new TapGestureRecognizer
                                                        {
                                                            Command = new Command(async () =>
                                                            {
                                                                Producto_vendedor vendedorElegido = new Producto_vendedor() ;
                                                                vendedorElegido.NicknameVendedor = vendedorPicker.SelectedItem.ToString();
                                                                caracteristicas = tallaPicker.SelectedItem.ToString();
                                                                ItemCarrito item = new ItemCarrito(vendedorElegido.Id ,1,service.GetUsuarioLogueado(),caracteristicas);
                                                                await service.AgregarItemCarrito(item);
                                                                await service.EliminarProductoListaDeseos(producto);
                                                                await CargarProductosListaDeseos();
                                                            })
                                                        }
                                        }
                                },
                               2, 4
                            );


                foreach (string p in vendedoresDatos) Console.WriteLine( "HOLAAAAAAAAAAAAAA" + p );
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

    }
}