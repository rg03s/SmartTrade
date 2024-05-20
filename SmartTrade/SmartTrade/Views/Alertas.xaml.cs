using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alertas : ContentPage
    {
        STService service;
        public Alertas()
        {
            service = STService.Instance;
            InitializeComponent();

            Usuario user = service.GetUsuarioLogueado();
            ICollection<Producto> productos = user.AlertasProductosSinStock;
            StackLayout container = FindByName("stack_alertas") as StackLayout;
            if (productos.Count > 0)
            {
                container.Children.Clear();

                foreach (Producto p in productos)
                {
                    CrearAlerta(p, container);
                }
            } 
            
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            Console.WriteLine("Perfil");
        }

        private void CrearAlerta(Producto p, StackLayout container)
        {

            Frame frame = new Frame() { CornerRadius = 10, Margin = new Thickness(10, 10, 10, 10), Padding = new Thickness(10, 10, 10, 10), BackgroundColor = Color.White, BorderColor = Color.LightGray };
            container.Children.Add(frame);

            StackLayout stack_alerta = new StackLayout() {
                Orientation = StackOrientation.Horizontal, Margin = new Thickness(10, 10, 10, 10),
                Padding = new Thickness(10, 10, 10, 10), BackgroundColor = Color.White
            };

            frame.Content = stack_alerta;

            Image img = new Image() { Source = p.Imagen, WidthRequest = 100, HeightRequest = 100 };
            stack_alerta.Children.Add(img);

            StackLayout stack_mensaje = new StackLayout() { Orientation = StackOrientation.Vertical, Margin = new Thickness(10, 0, 0, 0) };
            stack_alerta.Children.Add(stack_mensaje);

            Label lbl_nombre = new Label() { Text = p.Nombre, FontSize = 20 };
            stack_mensaje.Children.Add(lbl_nombre);

            Label lbl_mensaje = new Label
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span { Text = "El producto del vendedor ", FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Color.DarkGray },
                        new Span { Text = p.Producto_Vendedor.First().NicknameVendedor, FontSize = 15, TextColor = Color.Blue },
                        new Span { Text = " de tu lista de deseos se ha quedado sin stock. Pruebe a mirar otro vendedor", FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Color.DarkGray }
                    }
                }
            };

            stack_mensaje.Children.Add(lbl_mensaje);

            container.Children.Add(stack_alerta);

            // Agregar el GestureRecognizer al StackLayout stack_alerta
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new ProductPage(p));
            };
            stack_alerta.GestureRecognizers.Add(tap);

        }

    }
}