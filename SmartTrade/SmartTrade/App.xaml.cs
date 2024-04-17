using SmartTrade.Entities;
using SmartTrade.Persistencia.Services;
using SmartTrade.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade
{
    public partial class App : Application
    {

        public App()
        {
           InitializeComponent();
           STService service = STService.Instance;

           //DependencyService.Register<MockDataStore>();
           //MainPage = new NavigationPage(new LoginPage(service));

           Categoria cat = new Categoria("Ropa");
           Producto producto = new Ropa("Camiseta VCF", "20%", "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcTuaY4YmJUgBY0CG0wToaFkYdiBelA7crNbnN7b-uLagjWJDvR4gGzZgiJYvI9p1e5foboieRWSDw_5nfs9AKcyidX85waJqbXg1scBg6qEybyHLS6zunnGYGCWmafdT-cR_Apseu4&usqp=CAc",
                "modelo3d", "Descripción camiseta VCF", 20, cat, "talla S", "Blanco", "Puma", "camiseta");

            producto.Producto_Vendedor = new System.Collections.Generic.List<Producto_vendedor>();

            Producto_vendedor pv = new Producto_vendedor(1, "UPVShop", 20, 69.99);
            Producto_vendedor pv2 = new Producto_vendedor(2, "TiendaEstafas", 33, 333);
            producto.Producto_Vendedor.Add(pv);
            producto.Producto_Vendedor.Add(pv2);
            MainPage = new ProductPage(service, producto);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
