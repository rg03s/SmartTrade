using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartTrade.Models;
using SmartTrade.Persistencia.Services;
using SmartTrade.Views;
using Supabase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartTrade.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        /*public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }*/
        #region Atributos
        private string email;
        private string clave;
        #endregion

        #region Propiedades
        public string txtemail
        {
            get { return email; }
           // set { SetValue(ref email, value); }
        }
        public string txtclave
        {
            get { return clave; }
           // set { SetValue(ref clave, value); }
        }

        #endregion

        #region Command
        public Command LoginCommand { get; }
        #endregion

        #region Metodo
        public async Task LoginUsuario()
        {
            var objusuario = new UserModel()
            {
                EmailField = email,
                PasswordField = clave,
            };
            try
            {
                string supabaseUrl = "https://apjeqdhvkthosokvpvma.supabase.co";
                string supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY");
                var autenticacion = new SupabaseClient(supabaseUrl, supabaseKey);
               // var authuser = await autenticacion.SignInWithEmailAndPasswordAsync(objusuario.EmailField.ToString(), objusuario.PasswordField.ToString());
                //corrige el error anterior para que funcione correctamente
                
                //string obtenertoken = authuser.AccessToken;

                STService service = STService.Instance;
                var Propiedades_NavigationPage = new NavigationPage(new ProductPage(service));

                Propiedades_NavigationPage.BarBackgroundColor = Color.RoyalBlue;
                App.Current.MainPage = Propiedades_NavigationPage;


            }
            catch (Exception)
            {

                await App.Current.MainPage.DisplayAlert("Advertencia", "Los datos introducidos son incorrectos o el usuario se encuentra inactivo.", "Aceptar");
                //await App.Current.MainPage.DisplayAlert("Advertencia", ex.Message, "Aceptar");
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel(INavigation navegar)
        {
           // Navigation = navegar;
            LoginCommand = new Command(async () => await LoginUsuario());

        }
        #endregion
    }
}
