using SmartTrade.Entities;
using SmartTrade.Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroVendedor : ContentPage
    {
        private STService service;
        public RegistroVendedor(STService service)
        {
            InitializeComponent();
            this.service = service;
        }


        //Manejadores del Campo Nombre
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

        //Manejadores del Campo Nickname
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

        //Manejadores del Campo Correo

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


        //Manejadores del Campo Password
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
            if(showPasswordCheckbox.IsChecked == true) Contraseña.IsPassword = false;
            else Contraseña.IsPassword=true; ;
        }


        //Manejadores del Campo Dirección 
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

        //Manejadores del Campo IBAN (Cuenta bancaria)
        private void IBAN_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(IBAN.Text))
            {
                IBAN.TextColor = Color.Gray;
            }
        }

        public Boolean IsValidoIBAN(string IBAN)
        {

            if (string.IsNullOrEmpty(IBAN) || IBAN.Length < 6|| !IBAN.StartsWith("ES")) return false;
            else return true;
        }
        private void IBAN_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(IBAN.Text)) { IBAN.TextColor = Color.Black; }
        }

        private void IBAN_Unfocused(object sender, FocusEventArgs e)
        {
            string texto = IBAN.Text;
            if (IsValidoIBAN(texto) == false) IbanNoValido.IsVisible = true;
            else IbanNoValido.IsVisible = false;
        }

        //cambiar la pagina a InicioSesion si no se está registrado
        private async void IniciarSesion_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            LoginPage inicioSesion = new LoginPage(service);
            await Navigation.PushAsync(inicioSesion);
        }



        //Si todos los campos están correctamente completados se registrará el usuario
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
            if (!IsValidoIBAN(IBAN.Text))
            {
                await DisplayAlert("Error", "Por favor, introduzca un IBAN válido.", "Aceptar");
                return;
            }
            if (!IsValidaContraseña(Contraseña.Text))
            {
                await DisplayAlert("Error", "Por favor, introduzca una contraseña válida.", "Aceptar");
                return;
            }
            else try
                {

                    Usuario usuarioNuevo = new Usuario(NombreUser.Text,Nombre.Text, Contraseña.Text, Direccion.Text, Correo.Text, datePicker.Date);
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