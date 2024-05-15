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

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisPedidos : ContentPage
    {
        STService service;
        public MisPedidos()
        {
            InitializeComponent();
            this.service = STService.Instance;
        }

        override protected void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando pedidos...");
            CargarPedidos();
            UserDialogs.Instance.HideLoading();
        }

        private async void CargarPedidos()
        {
            try
            {
                List<Pedido> pedidos = await service.GetPedidos();
                StackLayout stackLayout = this.FindByName<StackLayout>("listaPedidos");
                stackLayout.Children.Clear();

                if (pedidos.Count == 0)
                {
                    StackLayout stackLayoutPedidoVacio = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            new Label
                            {
                                Text = "No hay pedidos disponibles",
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                TextColor = Color.Black,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
                    };
                    stackLayout.Children.Add(stackLayoutPedidoVacio);
                    return;
                }
                else
                {
                    foreach (var pedido in pedidos)
                    {
                        CrearTarjetaPedido(pedido, stackLayout);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar los pedidos: {e.Message}");
            }
        }

        private void CrearTarjetaPedido(Pedido pedido, StackLayout stackLayout)
        {
            var stackProductos = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 5 };

            foreach (var item in pedido.Productos)
            {
                var productoCard = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Children =
                    {
                        new Image { Source = item.Producto.Imagen, HeightRequest = 50, WidthRequest = 50 },
                        new StackLayout
                        {
                            VerticalOptions = LayoutOptions.Center,
                            Spacing = 5,
                            Children =
                            {
                                new Label { Text = item.Producto.Nombre, FontAttributes = FontAttributes.Bold, TextColor = Color.Black },
                                new Label { Text = item.Producto.Descripcion, TextColor = Color.Gray, LineBreakMode = LineBreakMode.TailTruncation },
                            }
                        }
                    }
                };
                stackProductos.Children.Add(productoCard);
            }

            var pedidoCard = new Frame
            {
                BorderColor = Color.LightGray,
                CornerRadius = 10,
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                HasShadow = true,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 10,
                    Children =
                    {
                        stackProductos,
                        new Label { Text = $"Precio total: {pedido.PrecioTotal}€", FontAttributes = FontAttributes.Bold, TextColor = Color.Black },
                        new Label { Text = $"Estado del pedido: {pedido.Estado}", TextColor = Color.Black },
                        new Label { Text = $"Fecha estimada: {pedido.FechaEstimada:dd/MM/yyyy}", TextColor = Color.Black },
                        CrearBotonAccion(pedido)
                    }
                }
            };
            stackLayout.Children.Add(pedidoCard);
        }

        private View CrearBotonAccion(Pedido pedido)
        {
            Button button = null;

            if (pedido.Estado == "En preparación")
            {
                button = new Button
                {
                    Text = "Cancelar pedido",
                    BackgroundColor = Color.Red,
                    TextColor = Color.White,
                    CornerRadius = 10
                };

                button.Clicked += async (sender, args) =>
                {
                    var confirm = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                    {
                        Message = "¿Estás seguro de que quieres cancelar este pedido?",
                        OkText = "Sí",
                        CancelText = "No"
                    });

                    if (confirm)
                    {
                        await service.CancelarPedido(pedido.Id);
                        UserDialogs.Instance.Toast("Pedido cancelado", TimeSpan.FromSeconds(3));
                        CargarPedidos();
                    }
                };
            }
            else if (pedido.Estado == "Entregado")
            {
                button = new Button
                {
                    Text = "Devolver pedido",
                    BackgroundColor = Color.Orange,
                    TextColor = Color.White,
                    CornerRadius = 10
                };

                button.Clicked += async (sender, args) =>
                {
                    var confirm = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                    {
                        Message = "¿Estás seguro de que quieres devolver este pedido?",
                        OkText = "Sí",
                        CancelText = "No"
                    });

                    if (confirm)
                    {
                        await service.DevolverPedido(pedido.Id);
                        UserDialogs.Instance.Toast("Pedido marcado para devolución", TimeSpan.FromSeconds(3));
                        CargarPedidos();
                    }
                };
            }

            return button ?? new Label { Text = "" };
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
    }
}
