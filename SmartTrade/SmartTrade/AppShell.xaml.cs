using SmartTrade.ViewModels;
using SmartTrade.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartTrade
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
