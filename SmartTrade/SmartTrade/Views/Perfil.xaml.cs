using Microsoft.Extensions.DependencyInjection;
using SmartTrade.Logica.Services;
using SmartTrade.Persistencia.DataAccess;
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
    public partial class Perfil : ContentPage
    {
        STService service;
        public Perfil()
        {
            InitializeComponent();
            this.service = STService.Instance;
            HandlerBotones();
            HandlerEntries();
        }

        private void HandlerBotones()
        {
            HolaLabel.Text = "Hola, " + service.GetLoggedNickname();
            PuntosLabel.Text = "Puntos acumulados: " + service.GetPuntos(service.GetLoggedNickname()).Result;
        }

        private void HandlerEntries()
        {
            string nickname = service.GetLoggedNickname();
            UsuarioEntry.Text = nickname;
            ContraseñaEntry.Text = service.GetPassword(nickname).Result;
            EmailEntry.Text = service.GetEmail(nickname).Result;
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
                LoginPage login = new LoginPage();
                await Navigation.PopToRootAsync();
            }
        }
    }
}