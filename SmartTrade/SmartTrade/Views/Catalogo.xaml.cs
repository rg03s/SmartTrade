using System;
using System.Collections.Generic;
using System.Linq;
using SmartTrade.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using SmartTrade.Logica.Services;
using Acr.UserDialogs;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalogo : ContentPage
    {
        private ISTService service;
        private List<Producto> catalogoProductos = new List<Producto>();

        public Catalogo(ISTService service, List<Producto> productos)
        {
            InitializeComponent();
            this.service = service;
            this.catalogoProductos = productos;

            ConfigurarPickerFiltrado();

            SearchBar searchBar = (SearchBar)FindByName("searchBar");
            searchBar.TextChanged += OnBusqueda;

        }

        private void CargarProductos()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando productos...");
                UserDialogs.Instance.HideLoading();
                if (catalogoProductos.Count == 0)
                {
                    Debug.WriteLine("No se han encontrado productos");
                }
                else
                {
                    MostrarProductos(catalogoProductos);

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error al cargar los productos: {e.Message}");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarProductos();
        }

        private void ConfigurarPickerFiltrado()
        {
            try
            {
                Picker picker = (Picker)FindByName("picker_categorias");
                picker.Items.Add("Ropa");
                picker.Items.Add("Deporte");
                picker.Items.Add("Papeleria");
                picker.Items.Add("Tecnologia");
                picker.SelectedIndexChanged += (s, e) =>
                {
                    string categoria = picker.Items[picker.SelectedIndex];
                    List<Producto> productosFiltrados = catalogoProductos.Where(p => p.Categoria == categoria).ToList();
                    MostrarProductos(productosFiltrados);
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        private void OnBusqueda(object sender, EventArgs e)
        {

            SearchBar searchBar = (SearchBar)FindByName("searchBar");
            string busqueda = searchBar.Text.ToLower();


            Grid grid_productos = (Grid)FindByName("grid_productos");
            grid_productos.Children.Clear();

            List<Producto> productosFiltrados = catalogoProductos.Where(p => p.Nombre.ToLower().Contains(busqueda)).ToList();

            if (busqueda == "")
            {
                productosFiltrados = catalogoProductos;
                grid_productosDestacados.IsVisible = true;
            }
            else
            {
                grid_productosDestacados.IsVisible = false;
            }

            MostrarProductos(productosFiltrados);
        }

        private void MostrarProductos(List<Producto> productos)
        {

            try
            {
                int columnasPorFila = 2;
                int filaActual = 0;
                int columnaActual = 0;

                Grid grid_productosDestacados = (Grid)FindByName("grid_productosDestacados");

                grid_productos.Children.Clear();
                grid_productos.RowDefinitions.Clear();


                if (productos.Count == 0)
                {
                    grid_productosDestacados.IsVisible = false;
                    grid_productos.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    Label label = new Label { Text = "No se han encontrado productos", FontSize = 20, TextColor = Color.Black };
                    grid_productos.Children.Add(label);
                    return;
                }

                for (int i = 0; i < Math.Ceiling((double)productos.Count / columnasPorFila); i++)
                {
                    grid_productos.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }


                foreach (Producto producto in productos)
                {

                    Producto_vendedor producto_vendedor = producto.Producto_Vendedor.OrderBy(pv => pv.Precio).First();

                    Frame frame = new Frame
                    {
                        HeightRequest = 180,
                        Margin = new Thickness(0, 0, 5, 0),
                        CornerRadius = 10,
                        BackgroundColor = Color.White,
                        Padding = 10
                    };

                    grid_productos.Children.Add(frame, columnaActual, filaActual);
                    columnaActual++;
                    if (columnaActual >= columnasPorFila)
                    {
                        columnaActual = 0;
                        filaActual++;
                        // Añade una nueva definición de fila si es necesario
                        grid_productos.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    }

                    TapGestureRecognizer tap = new TapGestureRecognizer();
                    tap.Tapped += (s, ev) =>
                    {
                        ProductPage productPage = new ProductPage(service, producto);
                        Navigation.PushAsync(productPage);
                    };

                    frame.GestureRecognizers.Add(tap);

                    StackLayout stackLayout = new StackLayout
                    {
                        Children =
                        {
                            new Image
                            {
                                Source = new UriImageSource { Uri = new Uri(producto.Imagen) },
                                Aspect = Aspect.AspectFill,
                                HeightRequest = 120
                            },
                            new Label
                            {
                                Text = producto.Nombre,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.Black
                            },
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {

                                    new Label
                                    {
                                        Text = producto.Huella_eco,
                                        FontAttributes = FontAttributes.Bold,
                                        TextColor = Color.Green
                                    },
                                    new Image
                                    {
                                        Source = new UriImageSource { Uri = new Uri("https://i.ibb.co/NZ99Tp4/Huella-Eco.png") },
                                        Aspect = Aspect.AspectFill,
                                        HeightRequest = 15
                                    },
                                    new Label
                                    {
                                        Text = "Desde " + producto_vendedor.Precio.ToString() + "€",
                                        FontAttributes = FontAttributes.Bold,
                                        TextColor = Color.Black,
                                        HorizontalOptions = LayoutOptions.EndAndExpand
                                    }
                                }
                            }
                        }
                    };

                    frame.Content = stackLayout;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void BtnCarrito_click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carrito(service));
        }

        public void BtnPerfil_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Perfil");
        }

        private void BtnDeseos_click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaDeseos(service));
        }
    }
}