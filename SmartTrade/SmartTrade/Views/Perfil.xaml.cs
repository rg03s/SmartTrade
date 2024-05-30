using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SmartTrade.Entities;
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
            //TODO: Acceder a la página de pedidos del usuario
        }

        private async void BtnTarjetas_click(object sender, EventArgs e)
        {
            //TODO
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
            if (UsuarioEntry.Text == service.GetLoggedNickname() && ContraseñaEntry.Text == service.GetLoggedPassword() && EmailEntry.Text == service.GetLoggedEmail())
                await DisplayAlert("Error", "No se ha realizado ningún cambio!", "ACEPTAR");
            else 
            {
                if (!await DisplayAlert("Confirmación", "¿Está seguro de que desea realizar cambios en su cuenta?", "SI", "NO")) return;
                Usuario usuario = service.GetLoggedUser();
                
                if (UsuarioEntry.Text != service.GetLoggedNickname())
                {
                    // Hace falta cambiar todas las claves ajenas al nickname del usuario antes de poder cambiarlo
                    // en la base de datos. Creo que ya sé para qué sirven las ids numéricas.
                    List<ItemCarrito> carrito = await service.GetCarrito();
                    List<ListaDeseosItem> listaDeseos = await service.GetListaDeseos(service.GetLoggedNickname());
                    List<Pedido> pedidos = await service.GetPedidos();
                    List<Producto_vendedor> productosVendedor = await service.GetLoggedProductosVendedor();
                    List<Tarjeta> tarjetas = await service.GetLoggedTarjetas();
                    foreach (ItemCarrito ic in carrito)
                    {
                        ic.NicknameUsuario = UsuarioEntry.Text;
                    }

                    foreach (ListaDeseosItem ldi in listaDeseos)
                    {
                        ldi.NickPropietario = UsuarioEntry.Text;
                    }

                    foreach (Pedido p in pedidos)
                    {
                        p.NickComprador = UsuarioEntry.Text;
                    }

                    foreach (Producto_vendedor pv in productosVendedor)
                    {
                        pv.NicknameVendedor = UsuarioEntry.Text;
                    }

                    foreach (Tarjeta t in tarjetas)
                    {
                        t.Nick_comprador = UsuarioEntry.Text;
                    }
                    
                    usuario.Nickname = UsuarioEntry.Text;
                }
                if (ContraseñaEntry.Text != service.GetLoggedPassword())
                {
                    usuario.Password = ContraseñaEntry.Text;
                }
                if (EmailEntry.Text != service.GetLoggedEmail())
                {
                    usuario.Email = EmailEntry.Text;
                }
                service.SaveChanges();
                await DisplayAlert("Éxito", "Cambios realizados con éxito", "ACEPTAR");
            }
        }
    }
}