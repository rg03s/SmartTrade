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
        public Alertas(ISTService service)
        {

            InitializeComponent();

            Usuario user = service.GetUsuarioLogueado();
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>(user.AlertasProductosSinStock);
            ListView lista = new ListView();
            lista.ItemsSource = productos;
            StackLayout stack = (StackLayout)FindByName("stack_alertas");
            stack.Children.Add(lista);

            lista.ItemSelected += (sender, e) =>
            {
                Producto p = (Producto)e.SelectedItem;
                Navigation.PushAsync(new ProductPage(service, p));
            };
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            Console.WriteLine("Perfil");
        }

    }
}