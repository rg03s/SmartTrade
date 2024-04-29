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

        private async void MostrarProductosCarrito(List<ItemCarrito> carrito)
        {

            StackLayout stackLayout = this.FindByName<StackLayout>("listaItems");

            foreach (ItemCarrito item in carrito)
            {
                try
                {
                    Producto producto = await service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
                    Producto_vendedor productoVendedor = producto.Producto_Vendedor.Where(pv => pv.Id == item.idProductoVendedor).FirstOrDefault();


                    Frame frame = new Frame
                    {
                        BorderColor = Color.LightGray,
                        CornerRadius = 10,
                        Padding = 10,
                        BackgroundColor = Color.Black
                    };

                    Grid grid = new Grid
                    {
                        RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto }
                    },
                        ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                    };

                    Image image = new Image
                    {
                        Source = producto.Imagen,
                        Aspect = Aspect.AspectFit
                    };

                    Label nombre = new Label
                    {
                        Text = producto.Nombre,
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold
                    };

                    Label precio = new Label
                    {
                        Text = productoVendedor.Precio.ToString(),
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold
                    };

                    Button btnSumar = new Button
                    {
                        Text = "+",
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black
                    };

                    Button btnRestar = new Button
                    {
                        Text = "-",
                        BackgroundColor = Color.LightGray,
                        TextColor = Color.Black
                    };

                    Label cantidad = new Label
                    {
                        Text = item.Cantidad.ToString(),
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold
                    };

                    btnSumar.Clicked += BtnSumar_click;
                    btnRestar.Clicked += BtnRestar_click;

                    grid.Children.Add(image, 0, 0);
                    grid.Children.Add(nombre, 1, 0);
                    grid.Children.Add(precio, 2, 0);
                    grid.Children.Add(btnSumar, 0, 1);
                    grid.Children.Add(cantidad, 1, 1);
                    grid.Children.Add(btnRestar, 2, 1);

                    frame.Content = grid;
                    stackLayout.Children.Add(frame);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al obtener el producto: {e.Message}");
                }

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