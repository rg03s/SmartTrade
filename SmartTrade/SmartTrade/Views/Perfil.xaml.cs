using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using SmartTrade.Persistencia.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        STService service;
        Usuario usuario;
        public Perfil()
        {
            InitializeComponent();
            this.service = STService.Instance;
            this.usuario = service.GetUsuarioLogueado();
            HandlerBotones();
            HandlerEntries();
        }

        private void HandlerBotones()
        {
            HolaLabel.Text = "Hola, " + service.GetLoggedNickname();
            PuntosLabel.Text = "Puntos acumulados: " + service.GetPuntos().Result;
        }

        private void HandlerEntries()
        {
            string nickname = service.GetLoggedNickname();
            UsuarioEntry.Text = nickname;
            ContraseñaEntry.Text = service.GetPassword(nickname).Result;
            EmailEntry.Text = service.GetEmail(nickname).Result;
        }

        public void ModificarContraseña(string nuevaContraseña)
        {
            this.usuario.Password = nuevaContraseña;
        }

        public void ModificarEmail(string nuevoEmail)
        {
            this.usuario.Email = nuevoEmail;
        }

        private void VerContraseña_Changed(object sender, TextChangedEventArgs e)
        {
            if (verPass.IsChecked == true) ContraseñaEntry.IsPassword = false;
            else ContraseñaEntry.IsPassword = true;
        }

        private void BtnAtras_click(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void BtnLogout_click(object sender, EventArgs e)
        {
            var confirmacion = await DisplayAlert("Confirmación", "¿Desea cerrar sesión de su cuenta?", "SI", "NO");
            if (confirmacion){
                this.service.Logout();
                await Navigation.PopToRootAsync();
            }
        }
        
        private async void BtnPedidos_click(object sender, EventArgs e)
        {
            ResumenPedidos resumenPedidos = new ResumenPedidos();
            await Navigation.PushAsync(resumenPedidos);
        }

        private async void BtnTarjetas_click(object sender, EventArgs e)
        {
            Tarjetas tarjetas = new Tarjetas();
            await Navigation.PushAsync(tarjetas);
        }

        private async void BtnMasTarde_click(object sender, EventArgs e)
        {
            GuardarMasTarde masTarde = new GuardarMasTarde();
            await Navigation.PushAsync(masTarde);
        }

        private async void BtnDeseos_click(object sender, EventArgs e)
        {
            ListaDeseos deseos = new ListaDeseos();
            await Navigation.PushAsync(deseos);
        }

        private async void BtnModificar_click(object sender, EventArgs e)
        {
            if (ContraseñaEntry.Text == service.GetLoggedPassword() && EmailEntry.Text == service.GetLoggedEmail())
            {
                await DisplayAlert("Error", "¡No se ha realizado ningún cambio!", "ACEPTAR"); return;
            }
            if (!IsValidoCorreo(EmailEntry.Text))
            {
                await DisplayAlert("Error", "¡El email proporcionado no es válido!", "ACEPTAR"); return;
            }
            
            if (!await DisplayAlert("Confirmación", "¿Está seguro de que desea realizar cambios en su cuenta?", "SI", "NO")) return;
            
            if (ContraseñaEntry.Text != service.GetLoggedPassword())
            {
                ModificarContraseña(ContraseñaEntry.Text);
            }
            if (EmailEntry.Text != service.GetLoggedEmail())
            {
                ModificarEmail(EmailEntry.Text);
            }
            service.SaveChanges();
            await DisplayAlert("Éxito", "Cambios realizados con éxito", "ACEPTAR");
            
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
    }
}