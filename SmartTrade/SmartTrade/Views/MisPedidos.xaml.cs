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
    {/*
        STService service;
        Pedido miPedido;
        public MisPedidos(Pedido pedido)
        {
            InitializeComponent();
            this.service = STService.Instance;
            miPedido = pedido;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando...");
            
            UserDialogs.Instance.HideLoading();
            if (miPedido != null)
            {
                // Cargar los detalles del pedido
                await CargarDetallesPedido(miPedido);
            }
            else
            {
                await DisplayAlert("Error", "No se pudo cargar el pedido", "OK");
            }
        }

        private async Task CargarDetallesPedido(Pedido pedido)
        {
            try
            {
                // Cargar los detalles del pedido
                await service.CargarDetallesPedido(pedido);
                MostrarDetallesPedido(pedido);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo cargar los detalles del pedido", "OK");
                Console.WriteLine($"Error al cargar los detalles del pedido: {ex.Message}");
            }
        }

        private void MostrarDetallesPedido(Pedido pedido)
        {
            var stackLayout = this.FindByName<StackLayout>("listaPedidos");

            foreach (var producto in pedido.ItemsCarrito)
            {
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
                                Source = service.GetImagenPedido(pedido),
                                HeightRequest = 100,
                                WidthRequest = 100,
                                Aspect = Aspect.AspectFit
                            },
                            new StackLayout
                            {
                                VerticalOptions = LayoutOptions.Center,
                                Spacing = 5,
                                Children =
                                {
                                    new Label { Text = service.GetNombreProductoPedido(producto), FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), TextColor = Color.Black, FontAttributes = FontAttributes.Bold },
                                    new Label { Text = service.GetDescripcionProductoPedido(producto).Length > 50 ? service.GetDescripcionProductoPedido(producto).Substring(0, 50) + "..." : service.GetDescripcionProductoPedido(producto), TextColor = Color.Gray },
                                    new Label { Text = service.GetPrecioProductoPedido(producto).ToString("F2") + "€", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black }
                                }
                            }
                        }
                    }
                };

                stackLayout.Children.Add(productCard);
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
        */
    }
}
