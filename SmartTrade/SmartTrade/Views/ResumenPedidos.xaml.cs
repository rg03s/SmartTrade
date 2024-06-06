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
    public partial class ResumenPedidos : ContentPage
    {
        STService service;
        List<Pedido> listaPedidos = new List<Pedido>();
        public ResumenPedidos()
        {
            InitializeComponent();
            this.service = STService.Instance;
        }

        override async protected void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando pedidos...");
            await MostrarPedidos();
            UserDialogs.Instance.HideLoading();
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async Task MostrarPedidos()
        {
            listaPedidos = await service.GetPedidos();
            StackLayout StackPedidos = (StackLayout)FindByName("StackPedidos");
            StackPedidos.Children.Clear();

            if (listaPedidos.Count == 0)
            {
                Label label = new Label { Text = "No se han encontrado pedidos", FontSize = 20, TextColor = Color.Black, Margin = new Thickness(30, 30, 0, 30) };
                StackPedidos.Children.Add(label);
                return;
            }

            int numPedido = 0;
            foreach (Pedido p in listaPedidos)
            {
                numPedido++;
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (s, ev) =>
                {
                    MisPedidos misPedidos = new MisPedidos(p);
                    await Navigation.PushAsync(misPedidos);
                };

                Frame frame = new Frame
                {
                    HeightRequest = 100,
                    WidthRequest = 400,
                    Margin = new Thickness(15, 0, 15, 15),
                    CornerRadius = 10,
                    BackgroundColor = Color.White,
                    Padding = 10,
                    HasShadow = true,
                    GestureRecognizers = { tap }
                };

                StackPedidos.Children.Add(frame);

                StackLayout stackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Image
                        {
                            Source = await service.GetImagenPedido(p),
                            Aspect = Aspect.AspectFill,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Orientation = StackOrientation.Vertical,
                            Children =
                            {
                                new Label
                                {
                                    Text = "Pedido " + numPedido,
                                    TextColor = Color.SlateGray,
                                    FontSize = 20
                                },

                                new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Children =
                                    {
                                        new Label
                                        {
                                            Text = "Fecha: " + p.Fecha,
                                            TextColor = Color.Gray,
                                            FontSize = 12
                                        },
                                        new Label
                                        {
                                            Text = p.Precio_total + "€",
                                            FontSize = 18,
                                            TextColor = Color.Black
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                frame.Content = stackLayout;
            }
        }
    }
}