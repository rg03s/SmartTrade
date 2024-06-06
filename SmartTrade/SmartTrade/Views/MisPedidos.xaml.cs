using Acr.UserDialogs;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisPedidos : ContentPage
    {
        STService service;
        Pedido miPedido;
        List<Producto> productos;
        public MisPedidos(Pedido pedido)
        {
            InitializeComponent();
            this.service = STService.Instance;
            miPedido = pedido;
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Console.WriteLine("Atras");
            Catalogo catalogo = new Catalogo();
            Navigation.PushAsync(catalogo);
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            Console.WriteLine("Perfil");
            Perfil perfil = new Perfil();
            Navigation.PushAsync(perfil);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando pedidos...");
            await CargarProductosPedido(miPedido);
            UserDialogs.Instance.HideLoading();
        }

        private async Task CargarProductosPedido(Pedido pedido)
        {
            try
            {
                Console.WriteLine("Cargando productos del pedido...");
                productos = await service.GetProductosPedido(pedido);
                Console.WriteLine($"Productos del pedido: {productos.Count}");
                StackLayout stackLayout = this.FindByName<StackLayout>("listaPedidos");
                StackLayout stack_resumen = this.FindByName<StackLayout>("stack_resumen");

                stackLayout.Children.Clear();

                if (productos.Count == 0)
                {
                    stack_resumen.IsVisible = false;

                    StackLayout stack = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            new Label
                            {
                                Text = "No hay pedidos",
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                TextColor = Color.Black,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
                    };
                    stackLayout.Children.Add(stack);
                    return;
                }
                else
                {
                    stack_resumen.IsVisible = true;
                    MostrarDetallesPedido(pedido);
                }
            }
            catch (ServiceException err)
            {
                UserDialogs.Instance.Alert("Error al cargar los productos del pedido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los productos del pedido: {ex.Message}");
                //await DisplayAlert("Error", "No se pudo cargar los productos del pedido", "OK");
                //Console.WriteLine($"Error al cargar los productos del pedido: {ex.Message}");
            }
            
        }

        private void MostrarDetallesPedido(Pedido pedido)
        {
            var stackLayout = this.FindByName<StackLayout>("listaPedidos");

            foreach (var producto in pedido.IdProductoVendedor)
            {
                crearTarjetaAsync(producto, stackLayout);
            }
            
            // Actualizar resumen
            this.FindByName<Label>("span_costeTotal").Text = pedido.Precio_total.ToString("F2") + "€";
            service.ActualizarEstadoPedido(pedido);
            string estado = pedido.Estado;
            this.FindByName<Label>("span_estadoPedido").Text = estado;
            this.FindByName<Label>("span_fechaEstimada").Text = pedido.Fecha.AddDays(5).ToString("dd/MM/yyyy");

            // Mostrar u ocultar botón de devolver según el estado
            var btnDevolverPedido = this.FindByName<Button>("btnDevolverPedido");
            if (estado == "Entregado")
            {
                btnDevolverPedido.IsVisible = true;
            }
            else
            {
                btnDevolverPedido.IsVisible = false;
            }
        }

        private async Task crearTarjetaAsync(int idProductoVendedor, StackLayout stackLayout)
        {

            Console.WriteLine($"Creando tarjeta del producto {idProductoVendedor}");
            try
            {
                Producto producto = productos.Find(p => p.Producto_Vendedor.First().Id == idProductoVendedor);
                Console.WriteLine($"Producto encontrado: {producto.Nombre}");
                string precio = producto.Producto_Vendedor.First().Precio.ToString("F2") + "€";
                Console.WriteLine($"Precio: {precio}");

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
                                            await Navigation.PushAsync(new ProductPage( producto));
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
                                   new Label{
                                        Text = producto.Nombre,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        TextColor = Color.Black,
                                        FontAttributes = FontAttributes.Bold
                                    },
                                    new Label{
                                        Text = producto.Descripcion.Length > 50 ? producto.Descripcion.Substring(0, 50) + "..." : producto.Descripcion,
                                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                        TextColor = Color.Black
                                    },
                                    new Label{
                                        Text = precio,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        TextColor = Color.Black,
                                        FontAttributes = FontAttributes.Bold
                                    }
                                }
                            }
                        }
                    }
                };
                stackLayout.Children.Add(productCard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tarjeta del producto: {ex.Message}");
            }
            
        }

        private async void BtnCancelarPedido_click(object sender, EventArgs e)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "¿Desea cancelar el pedido?",
                OkText = "Sí",
                CancelText = "No"
            });

            if (result)
            {
                try
                {
                    await service.CancelarPedido(miPedido);
                    UserDialogs.Instance.Toast("Pedido cancelado", TimeSpan.FromSeconds(3));
                    //eliminar todos los productos del pedido para que ya no se muestre ninguno
                    StackLayout stackLayout = this.FindByName<StackLayout>("listaPedidos");
                    stackLayout.Children.Clear();
                    //restableces la tarjeta de resumen toda vacía
                    this.FindByName<Label>("span_costeTotal").Text = "0.00€";
                    this.FindByName<Label>("span_estadoPedido").Text = "";
                    this.FindByName<Label>("span_fechaEstimada").Text = "";
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "No se pudo cancelar el pedido", "OK");
                    Console.WriteLine($"Error al cancelar el pedido: {ex.Message}");
                }
            }
        }
        
        private async void BtnDevolverPedido_click(object sender, EventArgs e)
        {
            if (miPedido.Estado == "Entregado")
            {
                btnDevolverPedido.IsVisible = true;
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Message = "¿Desea devolver el pedido?",
                    OkText = "Sí",
                    CancelText = "No"
                });

                if (result)
                {
                    try
                    {
                        await service.DevolverPedido(miPedido);
                        UserDialogs.Instance.Toast("Pedido pendiente de devolución", TimeSpan.FromSeconds(3));
                        await Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", "No se pudo procesar la devolución", "OK");
                        Console.WriteLine($"Error al devolver el pedido: {ex.Message}");
                    }
                }

            }
            
        }
        
    }
}
