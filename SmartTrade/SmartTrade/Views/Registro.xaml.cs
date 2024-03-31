using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;
using SmartTrade.Persistencia.Services;
using Supabase;

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

        private void Button_Clicked(object sender, EventArgs e)
        {
           
            Usuario nuevoUsuario = new Usuario("ejempl", "ejem1", "1234", "abcd", "ejem1@gmail.com", DateTime.Now);
            service.AddUser(nuevoUsuario);
            
            
        }
        
    }
}