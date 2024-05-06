using SmartTrade.Entities;
using SmartTrade.Fabrica;
using SmartTrade.Logica.Fabrica;
using SmartTrade.Logica.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarProducto : ContentPage
    {
        STService service;

        public AgregarProducto(STService service)
        {
            InitializeComponent();
            this.service = service;

            ConfigurarPickerFiltrado();
        }

        private void ConfigurarPickerFiltrado()
        {
            try
            {
                Picker picker = (Picker)FindByName("picker");
                picker.Items.Add("Ropa");
                picker.Items.Add("Deporte");
                picker.Items.Add("Papeleria");
                picker.Items.Add("Tecnologia");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void Nombre_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Nombre.Text))
            {
                Nombre.TextColor = Color.Gray;
            }
        }

        private void Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Nombre.Text)) { Nombre.TextColor = Color.Black; }
        }

        private void ButtonImagen_Clicked(object sender, EventArgs e)
        {
            Imagen.Source = ImagenURL.Text;
        }

        private async void AgregarProducto_Clicked(object sender, EventArgs e)
        {
            if (!StockPrecioSonInt()) await DisplayAlert("Error", "El stock y el precio del producto deben ser números!", "OK");
            else {
                Producto producto = new Producto(Nombre.Text, "0", ImagenURL.Text, "", Descripcion.Text, 0, picker.Items[picker.SelectedIndex]);
                await service.AddProducto(producto);
            }
        }

        private void Precio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Precio.Text.All(Char.IsDigit))
            {
                Precio.TextColor = Color.Red;
            } else
            {
                Precio.TextColor = Color.Gray;
            }
        }

        private void Stock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Stock.Text.All(Char.IsDigit))
            {
                Stock.TextColor = Color.Red;
            }
            else
            {
                Stock.TextColor = Color.Gray;
            }
        }

        private bool StockPrecioSonInt()
        {
            if (!Precio.Text.All(Char.IsDigit) || !Stock.Text.All(Char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}