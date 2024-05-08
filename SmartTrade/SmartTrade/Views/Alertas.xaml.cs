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
            Usuario user = service.GetUsuarioLogueado();
            ObservableCollection<Producto> productos = new ObservableCollection<Producto>(user.AlertasProductosSinStock);
            ListView lista = new ListView();
            lista.ItemsSource = productos;
            lista.Parent = this.FindByName<StackLayout>("stack_alertas");

            lista.ItemSelected += (sender, e) =>
            {
                Producto p = (Producto)e.SelectedItem;
                Navigation.PushAsync(new ProductPage(service, p));
            };

            InitializeComponent();

        }

    }
}