using SmartTrade.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;
using Supabase;
using SmartTrade.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Diagnostics;
using System.Security.Cryptography;
using SmartTrade.Logica.Services;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalogo : ContentPage
    {
        private STService service;
        private List<Producto> catalogoProductos;

        public Catalogo(STService service)
        {
            InitializeComponent();
            this.service = service;

            SearchBar searchBar = (SearchBar)FindByName("searchBar");
            searchBar.TextChanged += onBusqueda;

            catalogoProductos = crearProductos();
            mostrarProductos(catalogoProductos);
        }
        private async void onCamisetaTapped(object sender, EventArgs e)
        {
            Producto camiseta = crearCamiseta();
            ProductPage paginaProducto = new ProductPage(service, camiseta);
            await Navigation.PushAsync(paginaProducto);
        }

        private async void onPelotaTapped(object sender, EventArgs e)
        {
            Producto pelota = crearPelota();
            ProductPage paginaProducto = new ProductPage(service, pelota);
            await Navigation.PushAsync(paginaProducto);
        }

        private async void onCuadernoTapped(object sender, EventArgs e)
        {
            Producto cuaderno = crearCuadernos();
            ProductPage paginaProducto = new ProductPage(service, cuaderno);
            await Navigation.PushAsync(paginaProducto);
        }

        private async void onGBCTapped(object sender, EventArgs e)
        {
            Producto gbc = crearGBC();
            ProductPage paginaProducto = new ProductPage(service, gbc);
            await Navigation.PushAsync(paginaProducto);
        }

        private List<Producto> crearProductos()
        {
            List<Producto> productos = new List<Producto>();
            
            Ropa p1 = new Ropa("Camiseta Valencia CF", "30%", "https://i.ibb.co/d7vYJM6/Camiseta-Valencia.jpg", "",
                                            "Camiseta del Valencia CF en muy buen estado de segunda mano.\n\nTalla M.", 10, new Categoria("Ropa").Nombre, "M",
                                                "Blanca", "Deporte", "Camiseta");

            Deporte p2 = new Deporte("Pelota Baloncesto", "50%", "https://i.ibb.co/LNSNFFf/Pelota-Baloncesto.png", "",
                                        "Pelota de Baloncesto de mi hijo. Le gustaba mucho pero se murió. La vendo barata.", 20, new Categoria("Deporte").Nombre, "Pelota");

            Papeleria p3 = new Papeleria("Cuaderno de colores", "75%", "https://i.ibb.co/qkQKMpc/Cuaderno-Colores.png", "",
                                            "Cuadernos muy bonitos del color que elijas. Muy buena calidad.", 20, new Categoria("Papeleria").Nombre, "Plástico");

            Tecnologia p4 = new Tecnologia("GameBoy Color", "20%", "https://i.ibb.co/sC5pJzS/GBC.png", "",
                                            "GameBoy Color muy antigua. Funciona más o menos pero un pokemon te echas tranquilamente.", 1, new Categoria("Tecnologia").Nombre,
                                                  "Consola", "Nintendo", "GameBoy Color");

            p1.Producto_Vendedor = new List<Producto_vendedor> { 
                new Producto_vendedor(10, "ValenciaFan", 10, 79.99),
                new Producto_vendedor(11, "ValenciaOutlet", 1, 34.99),
                new Producto_vendedor(12, "UPVShop", 1, 69.99)
            };
            p2.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(11, "Lebron James", 1, 4.99) };
            p3.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(12, "PickMeGirl", 50, 12.99) };
            p4.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(13, "UltraNerd69", 1, 19.99) };

            productos.Add(p1);
            productos.Add(p2);
            productos.Add(p3);
            productos.Add(p4);

            return productos;
        }

        private Producto crearCamiseta()
        {
            Ropa p1 = new Ropa("Camiseta Valencia CF", "30%", "https://i.ibb.co/d7vYJM6/Camiseta-Valencia.jpg", "",
                                            "Camiseta del Valencia CF en muy buen estado de segunda mano.\n\nTalla M.", 10, new Categoria("Ropa").Nombre, "M",
                                                "Blanca", "Deporte", "Camiseta");

            p1.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(10, "ValenciaFan", 10, 79.99) };

            return p1;
        }

        private Producto crearPelota()
        {
            Deporte p2 = new Deporte("Pelota Baloncesto", "50%", "https://i.ibb.co/LNSNFFf/Pelota-Baloncesto.png", "",
                                        "Pelota de Baloncesto de mi hijo. Le gustaba mucho pero se murió. La vendo barata.", 20, new Categoria("Deporte").Nombre, "Pelota");

            p2.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(11, "Lebron James", 1, 4.99) };

            return p2;
        }

        private Producto crearCuadernos()
        {
            Papeleria p3 = new Papeleria("Cuaderno de colores", "75%", "https://i.ibb.co/qkQKMpc/Cuaderno-Colores.png", "",
                                            "Cuadernos muy bonitos del color que elijas. Muy buena calidad.", 20, new Categoria("Papeleria").Nombre, "Plástico");
            
            p3.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(12, "PickMeGirl", 50, 12.99) };

            return p3;
        }

        private Producto crearGBC()
        {
            Tecnologia p4 = new Tecnologia("GameBoy Color", "20%", "https://i.ibb.co/sC5pJzS/GBC.png", "",
                                            "GameBoy Color muy antigua. Funciona más o menos pero un pokemon te echas tranquilamente.", 1, new Categoria("Tecnologia").Nombre,
                                                  "Consola", "Nintendo", "GameBoy Color");

            p4.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(13, "UltraNerd69", 1, 19.99) };

            return p4;
        }

        private async void onBusqueda(object sender, EventArgs e)
        {

            SearchBar searchBar = (SearchBar)FindByName("searchBar");
            string busqueda = searchBar.Text.ToLower();


            Grid grid_productos = (Grid)FindByName("grid_productos");
            grid_productos.Children.Clear();

            catalogoProductos = crearProductos();

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

            mostrarProductos(productosFiltrados);
        }

        private void mostrarProductos(List<Producto> productos)
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
                tap.Tapped += (s, ev) => {
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
    }
}