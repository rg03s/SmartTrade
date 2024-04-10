using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;
using SmartTrade.Persistencia.Services;
using Supabase;
using SmartTrade.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private STService service;
        public Registro(STService service)
        {
            InitializeComponent();
            this.service = service;
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

        private void NombreUser_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(NombreUser.Text))
            {
                NombreUser.TextColor = Color.Gray;
            }
        }

        private void NombreUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NombreUser.Text)) { NombreUser.TextColor = Color.Black; }
        }

        private void Correo_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Correo.Text))
            {
                Correo.TextColor = Color.Gray;
            }
        }

        private void Correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Correo.Text)) { Correo.TextColor = Color.Black; }
        }

        private void Contraseña_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Contraseña.Text))
            {
                Contraseña.TextColor = Color.Gray;
            }
        }

        private bool IsValidoCorreo(string correo)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        private void Correo_Unfocused(object sender, FocusEventArgs e)
        {
            string texto = Correo.Text;

            if (!IsValidoCorreo(texto))
            {
                Correo.TextColor = Color.Red;
            }
        }


        public Boolean IsValidaContraseña(string contraseña)
        {

            if (string.IsNullOrEmpty(contraseña) || contraseña.Length < 8) return false;
            return contraseña.Any(char.IsDigit);
        }

        private void Contraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Contraseña.Text)) { Contraseña.TextColor = Color.Black; }
            string password = e.NewTextValue;
            if (IsValidaContraseña(password) == false)
            { ContraseñaNoValida.IsVisible = true; }
            else ContraseñaNoValida.IsVisible = false;

        }

        private void VerContraseña_Changed(object sender, TextChangedEventArgs e)
        {
            if (VerContraseña.IsChecked == true) Contraseña.IsPassword = false;
            else Contraseña.IsPassword = true; ;
        }

        private void Direccion_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Direccion.Text))
            {
                Direccion.TextColor = Color.Gray;
            }
        }

        private void Direccion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Direccion.Text)) { Direccion.TextColor = Color.Black; }
        }


        //cambiar la pagina a InicioSesion cuando esté
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
                LoginPage inicioSesion = new LoginPage(service);
            await Navigation.PushAsync(inicioSesion);
        }


        private async void Registrarse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nombre.Text) || string.IsNullOrWhiteSpace(Direccion.Text) || string.IsNullOrWhiteSpace(NombreUser.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "Aceptar");
                return;
            }

            // Validar el formato del correo electrónico
            if (!IsValidoCorreo(Correo.Text))
            {
                await DisplayAlert("Error", "Por favor, introduzca un correo electrónico válido.", "Aceptar");
                return;
            }
            if (!IsValidaContraseña(Contraseña.Text))
            {
                await DisplayAlert("Error", "Por favor, introduzca una contraseña válida.", "Aceptar");
                return;
            }
            else try
                {

                    Usuario usuarioNuevo = new Usuario(NombreUser.Text, Nombre.Text, Contraseña.Text, Direccion.Text, Correo.Text, datePicker.Date);
                    await service.AddUser(usuarioNuevo);
                    await Navigation.PopAsync();
                    ProductPage paginaPrincipal = new ProductPage(service);
                    await Navigation.PushAsync(paginaPrincipal);
                }
                catch (EmailYaRegistradoException ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");

                }
                catch (EmailFormatoIncorrectoException ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
                catch (NickYaRegistradoException ex)
                {
                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
        }
    }
}