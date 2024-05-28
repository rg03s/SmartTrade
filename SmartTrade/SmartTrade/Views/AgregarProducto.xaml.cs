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

                picker.SelectedIndexChanged += (s, e) =>
                {
                    string categoria = picker.Items[picker.SelectedIndex];
                    switch (categoria.ToLower())
                    {
                        case "ropa":
                            OcultarGrids();
                            GridRopa.IsVisible = true; break;
                        case "deporte":
                            OcultarGrids();
                            GridDeporte.IsVisible = true; break;
                        case "papeleria":
                            OcultarGrids();
                            GridPapeleria.IsVisible = true; break;
                        case "tecnologia":
                            OcultarGrids();
                            GridTecnologia.IsVisible = true; break;
                        default:
                            break;
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void OcultarGrids()
        {
            GridRopa.IsVisible = false;
            GridDeporte.IsVisible = false;
            GridPapeleria.IsVisible = false;
            GridTecnologia.IsVisible = false;
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

            Picker picker = (Picker)FindByName("picker");
            if (picker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Por favor, elija la categoría del producto", "Aceptar");
                return;
            }
            string categoria = picker.Items[picker.SelectedIndex];
            if (!StockPrecioSonValidos()) 
            { 
                await DisplayAlert("Error", "El stock y el precio del producto deben ser números!", "Aceptar");
                return;
            }
            if(string.IsNullOrWhiteSpace(Nombre.Text) || string.IsNullOrEmpty(Descripcion.Text) || string.IsNullOrEmpty(Stock.Text) || string.IsNullOrEmpty(Precio.Text) || string.IsNullOrEmpty(ImagenURL.Text))
            {
                await DisplayAlert("Error", "Por favor, rellene todos los campos", "Aceptar");
                return;
            }
            if(categoria.ToLower() == "ropa")
            {
                if (string.IsNullOrEmpty(EntryTalla.Text) || string.IsNullOrEmpty(EntryColor.Text) || string.IsNullOrEmpty(EntryMarcaRopa.Text) || string.IsNullOrEmpty(EntryTipoRopa.Text))
                {
                    await DisplayAlert("Error", "Por favor, rellene todos los campos", "Aceptar");
                    return;
                }
                if (!TallaEsValida())
                {
                    await DisplayAlert("Error", "Por favor, elija una talla válida (XS, S, M, L o XL)", "Aceptar");
                    return;
                }
            }
            if (categoria.ToLower() == "deporte")
            {
                if (string.IsNullOrEmpty(EntryTipoDeporte.Text))
                {
                    await DisplayAlert("Error", "Por favor, rellene todos los campos", "Aceptar");
                    return;
                }
            }
            if (categoria.ToLower() == "papeleria")
            {
                if (string.IsNullOrEmpty(EntryMaterial.Text))
                {
                    await DisplayAlert("Error", "Por favor, rellene todos los campos", "Aceptar");
                    return;
                }
            }
            if (categoria.ToLower() == "papeleria")
            {
                if (string.IsNullOrEmpty(EntryDispositivo.Text) || string.IsNullOrEmpty(EntryMarcaTecnologia.Text) || string.IsNullOrEmpty(EntryModelo.Text))
                {
                    await DisplayAlert("Error", "Por favor, rellene todos los campos", "Aceptar");
                    return;
                }
            }
            else {
                try
                {
                    Producto producto = new Producto();
                    
                    switch (categoria.ToLower())
                    {
                        case "ropa":
                            producto = new Ropa(Nombre.Text, "0%", ImagenURL.Text, "", Descripcion.Text, 0, categoria, EntryTalla.Text.ToUpper(), EntryColor.Text, EntryMarcaRopa.Text, EntryTipoRopa.Text);
                            break;
                        case "deporte":
                            producto = new Deporte(Nombre.Text, "0%", ImagenURL.Text, "", Descripcion.Text, 0, categoria, EntryTipoDeporte.Text);
                            break;
                        case "papeleria":
                            producto = new Papeleria(Nombre.Text, "0%", ImagenURL.Text, "", Descripcion.Text, 0, categoria, EntryMaterial.Text);
                            break;
                        case "tecnologia":
                            producto = new Tecnologia(Nombre.Text, "0%", ImagenURL.Text, "", Descripcion.Text, 0, categoria, EntryDispositivo.Text, EntryMarcaTecnologia.Text, EntryModelo.Text);
                            break;
                        default:
                            break;
                    }
                    await service.AddProducto(producto);
                    int stock = Int32.Parse(Stock.Text);
                    double precio = double.Parse(Precio.Text);
                    Producto_vendedor producto_vendedor = new Producto_vendedor(producto.Id, service.GetLoggedNickname(), stock, precio);
                    await service.AddProductoVendedor(producto_vendedor);
                    await DisplayAlert("Éxito", "Producto añadido!", "Aceptar");
                    List<Producto> productos = await service.GetProductosDeVendedor(service.GetLoggedNickname());
                    CatalogoVendedor paginaPrincipal = new CatalogoVendedor(service, productos);
                    await Navigation.PushAsync(paginaPrincipal);
                } catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error al añadir el producto", "Aceptar");
                }
            }
        }

        private bool TallaEsValida()
        {
            List<string> tallasValidas = new List<string>(){ "XS", "S", "M", "L", "XL" };
            return tallasValidas.Contains(EntryTalla.Text.ToUpper());
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
            if (string.IsNullOrEmpty(Precio.Text) || string.IsNullOrEmpty(Stock.Text)) return false;
            if ((!Precio.Text.All(Char.IsDigit) && !Precio.Text.Any(Char.IsPunctuation)) || !Stock.Text.All(Char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}