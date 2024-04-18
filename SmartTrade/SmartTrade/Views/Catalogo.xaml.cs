using SmartTrade.Persistencia.Services;
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

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalogo : ContentPage
    {
        private STService service;
        public Catalogo(STService service)
        {
            InitializeComponent();
            this.service = service;
            this.BindingContext = new CatalogoViewModel(service);
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

        private void crearProductos()
        {
            Ropa p1 = new Ropa("Camiseta Valencia CF", "30%", "https://i.ibb.co/d7vYJM6/Camiseta-Valencia.jpg", "",
                                            "Camiseta del Valencia CF en muy buen estado de segunda mano.\n\nTalla M.", 10, new Categoria("Ropa"), "M",
                                                "Blanca", "Deporte", "Camiseta");

            Deporte p2 = new Deporte("Pelota Baloncesto", "50%", "https://i.ibb.co/LNSNFFf/Pelota-Baloncesto.png", "",
                                        "Pelota de Baloncesto de mi hijo. Le gustaba mucho pero se murió. La vendo barata.", 20, new Categoria("Deporte"), "Pelota");

            Papeleria p3 = new Papeleria("Cuaderno de colores", "75%", "https://i.ibb.co/qkQKMpc/Cuaderno-Colores.png", "",
                                            "Cuadernos muy bonitos del color que elijas. Muy buena calidad.", 20, new Categoria("Papeleria"), "Plástico");

            Tecnologia p4 = new Tecnologia("GameBoy Color", "20%", "https://i.ibb.co/sC5pJzS/GBC.png", "",
                                            "GameBoy Color muy antigua. Funciona más o menos pero un pokemon te echas tranquilamente.", 1, new Categoria("Tecnologia"),
                                                  "Consola", "Nintendo", "GameBoy Color");

            p1.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(10, "ValenciaFan", 10, 79.99) };
            p2.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(11, "Lebron James", 1, 4.99) };
            p3.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(12, "PickMeGirl", 50, 12.99) };
            p4.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(13, "UltraNerd69", 1, 19.99) };
        }

        private Producto crearCamiseta()
        {
            Ropa p1 = new Ropa("Camiseta Valencia CF", "30%", "https://i.ibb.co/d7vYJM6/Camiseta-Valencia.jpg", "",
                                            "Camiseta del Valencia CF en muy buen estado de segunda mano.\n\nTalla M.", 10, new Categoria("Ropa"), "M",
                                                "Blanca", "Deporte", "Camiseta");

            p1.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(10, "ValenciaFan", 10, 79.99) };

            return p1;
        }

        private Producto crearPelota()
        {
            Deporte p2 = new Deporte("Pelota Baloncesto", "50%", "https://i.ibb.co/LNSNFFf/Pelota-Baloncesto.png", "",
                                        "Pelota de Baloncesto de mi hijo. Le gustaba mucho pero se murió. La vendo barata.", 20, new Categoria("Deporte"), "Pelota");

            p2.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(11, "Lebron James", 1, 4.99) };

            return p2;
        }

        private Producto crearCuadernos()
        {
            Papeleria p3 = new Papeleria("Cuaderno de colores", "75%", "https://i.ibb.co/qkQKMpc/Cuaderno-Colores.png", "",
                                            "Cuadernos muy bonitos del color que elijas. Muy buena calidad.", 20, new Categoria("Papeleria"), "Plástico");
            
            p3.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(12, "PickMeGirl", 50, 12.99) };

            return p3;
        }

        private Producto crearGBC()
        {
            Tecnologia p4 = new Tecnologia("GameBoy Color", "20%", "https://i.ibb.co/sC5pJzS/GBC.png", "",
                                            "GameBoy Color muy antigua. Funciona más o menos pero un pokemon te echas tranquilamente.", 1, new Categoria("Tecnologia"),
                                                  "Consola", "Nintendo", "GameBoy Color");

            p4.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(13, "UltraNerd69", 1, 19.99) };

            return p4;
        }
    }
}