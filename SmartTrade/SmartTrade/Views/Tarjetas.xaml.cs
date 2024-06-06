using SmartTrade.Logica.Services;
using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tarjetas : ContentPage
    {
        STService service;
        List<Tarjeta> listaTarjetas = new List<Tarjeta>();

        public Tarjetas()
        {
            InitializeComponent();
            this.service = STService.Instance;
            MostrarTarjetas();
        }

        private async void MostrarTarjetas()
        {
            listaTarjetas = await service.getTarjetas();
            StackLayout StackTarjetas = (StackLayout)FindByName("StackTarjetas");
            StackTarjetas.Children.Clear();

            if (listaTarjetas.Count == 0)
            {
                StackTarjetas.IsVisible = false;
                Label label = new Label { Text = "No se han encontrado tarjetas", FontSize = 20, TextColor = Color.Black, Margin = new Thickness(30,30,0,30)};
                StackTarjetas.Children.Add(label);
                return;
            }

            foreach (Tarjeta t in listaTarjetas)
            {
                string numTarjeta = t.Numero.ToString();
                string ultimosTarjeta = numTarjeta.Substring(numTarjeta.Length - 4);
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (s, ev) =>
                {
                    var confirmacion = await DisplayAlert("Confirmación", "¿Está seguro de que quiere eliminar esta tarjeta?", "SI", "NO");
                    if (confirmacion)
                    {
                        await service.BorrarTarjeta(t);
                        MostrarTarjetas();
                    }
                };

                Frame frame = new Frame
                {
                    HeightRequest = 30,
                    WidthRequest = 400,
                    Margin = new Thickness(15, 0, 15, 15),
                    CornerRadius = 10,
                    BackgroundColor = Color.White,
                    Padding = 10,
                    HasShadow = true
                };

                StackTarjetas.Children.Add(frame);

                StackLayout stackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Image
                        {
                            Source = new UriImageSource { Uri = new Uri("https://i.ibb.co/5R6WByW/image-2024-06-04-012400271.png") },
                            Aspect = Aspect.AspectFill,
                            HeightRequest = 20,
                            WidthRequest = 40,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new Label
                        {
                            Text = ("Tarjeta terminada en ****" + ultimosTarjeta),
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.Black,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        },
                        new Image
                        {
                            Source = new UriImageSource {Uri = new Uri("https://i.ibb.co/cTM9kwy/image-2024-06-04-020811797.png")},
                            Aspect = Aspect.AspectFill,
                            HeightRequest = 20,
                            WidthRequest = 25,
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            GestureRecognizers = {tap}
                        }
                    }
                };

                frame.Content = stackLayout;
            }
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void BtnAñadir_click(object sender, EventArgs e)
        {
            //TODO
            if (NumEntry.Text == null || SegEntry.Text == null || TitularEntry.Text == null)
            {
                await DisplayAlert("Error", "No pueden haber campos vacíos", "ACEPTAR");
                return;
            }
            
            if (NumEntry.Text.Length != 16 || SegEntry.Text.Length != 3)
            {
                await DisplayAlert("Error", "Los datos introducidos no son correctos", "ACEPTAR");
                return;
            }
            var confirmacion = await DisplayAlert("Confirmación", "¿Está seguro de que desea agregar la tarjeta?", "SI", "NO");
            if (confirmacion) {
                if (await service.NumTarjetaYaRegistradoEnMismoUsuario(NumEntry.Text))
                {
                    await DisplayAlert("Error", "Tarjeta ya registrada", "ACEPTAR");
                    return;
                }
                Tarjeta nuevaTarjeta = new Tarjeta(NumEntry.Text, CaducidadPicker.Date, Int32.Parse(SegEntry.Text), service.GetLoggedNickname());
                await service.AddTarjeta(nuevaTarjeta);
                UserDialogs.Instance.ShowLoading("Añadiendo tarjeta...");
                await Task.Delay(1000);
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Éxito", "Tarjeta añadida con éxito", "ACEPTAR");
                Navigation.PopAsync();
            }
        }

        private void NumEntry_unfocused(object sender, EventArgs e)
        {
            if (NumEntry.Text != null)
            {
                if (NumEntry.Text.Length != 16)
                {
                    NumEntry.TextColor = Color.Red;
                }
                else
                {
                    NumEntry.TextColor = Color.Default;
                }
            }
        }

        private void SegEntry_unfocused(object sender, EventArgs e)
        {
            if (SegEntry.Text != null)
            {
                if (SegEntry.Text.Length != 3)
                {
                    SegEntry.TextColor = Color.Red;
                }
                else
                {
                    SegEntry.TextColor = Color.Default;
                }
            }
        }
    }
}