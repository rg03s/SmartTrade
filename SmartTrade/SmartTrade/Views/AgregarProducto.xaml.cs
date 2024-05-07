using SmartTrade.Entities;
using SmartTrade.Fabrica;
using SmartTrade.Logica.Fabrica;
using SmartTrade.Logica.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (!StockPrecioSonValidos()) await DisplayAlert("Error", "El stock y el precio del producto deben ser números!", "OK");
            else {
                try
                {
                    Producto producto = new Producto(Nombre.Text, "0%", ImagenURL.Text, "", Descripcion.Text, 0, picker.Items[picker.SelectedIndex]);
                    await service.AddProducto(producto);
                    int stock = Int32.Parse(Stock.Text);
                    double precio = double.Parse(Precio.Text);
                    Producto_vendedor producto_vendedor = new Producto_vendedor(producto.Id, service.GetLoggedNickname(), stock, precio);
                    await service.AddProductoVendedor(producto_vendedor);
                    await DisplayAlert("Éxito", "Producto añadido!", "Aceptar"); 
                } catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error al añadir el producto", "Aceptar");
                    Debug.WriteLine("Error al añadir el producto: " + ex.Message);
                }
            }
        }

        private void Precio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Precio.Text.All(Char.IsDigit) && !Precio.Text.Any(Char.IsPunctuation))
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

        private bool StockPrecioSonValidos()
        {
            if ((!Precio.Text.All(Char.IsDigit) && !Precio.Text.Any(Char.IsPunctuation)) || !Stock.Text.All(Char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}