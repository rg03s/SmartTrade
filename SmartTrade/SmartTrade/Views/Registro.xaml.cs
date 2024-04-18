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
        //Manejadores Campo Nombre
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
        //Manejadores del Campo Apellido
        private void Apellido_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Apellido.Text))
            {
                Apellido.TextColor = Color.Gray;
            }
        }
        private void Apellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Apellido.Text)) { Apellido.TextColor = Color.Black; }
        }

        //Manejadores Campo Nickname
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

        //Manejadores Campo Correo electrónico
        private void Correo_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Correo.Text))
            {
                Correo.TextColor = Color.Gray;
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

        private void Correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Correo.Text)) { Correo.TextColor = Color.Black; }
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
        //Manejadores Campo Contraseña
        private void Contraseña_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Contraseña.Text))
            {
                Contraseña.TextColor = Color.Gray;
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

        //Manejadores de los Campos Dirección 
        private void Calle_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCalle.Text))
            {
                txtCalle.TextColor = Color.Gray;
            }
        }

        private void Numero_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                txtNumero.TextColor = Color.Gray;
            }
        }
        private void Calle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCalle.Text)) { txtCalle.TextColor = Color.Black; }
        }
        private void Numero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumero.Text)) { txtNumero.TextColor = Color.Black; }
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

            if (string.IsNullOrWhiteSpace(Nombre.Text) || string.IsNullOrWhiteSpace(txtCalle.Text) || string.IsNullOrWhiteSpace(txtNumero.Text) || pickerPais.SelectedItem == null || pickerCiudad.SelectedItem == null || string.IsNullOrWhiteSpace(NombreUser.Text))
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
            if (service.MayorDe18(fecha_nacimiento.Date) == false)
            {
                await DisplayAlert("Error", "Debes ser mayor de 18 años", "Aceptar");
                return;
            }
            if (!txtNumero.Text.All(char.IsDigit))
                {
                await DisplayAlert("Error", "Introduce un número de calle válido", "Aceptar");
                return;
            }
            else try
                {
                    string paisSeleccionado = pickerPais.SelectedItem.ToString();
                    string ciudadSeleccionada = pickerCiudad.SelectedItem.ToString();
                    string Direccion = txtCalle.Text + "," + txtNumero.Text + "," + ciudadSeleccionada + "," + paisSeleccionado;
                    Usuario compradorNuevo = new Usuario(NombreUser.Text, Nombre.Text, Contraseña.Text, Direccion, Correo.Text, fecha_nacimiento.Date, false);
                    compradorNuevo.AddDatosComprador();
                    await service.AddUser(compradorNuevo);
                     
                   // await Navigation.PopAsync();
                   //CAMBIAR A CATÁLOGO
                    ProductPage paginaPrincipal = new ProductPage(service, null);
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