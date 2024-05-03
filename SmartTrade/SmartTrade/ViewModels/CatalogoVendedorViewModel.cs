using SmartTrade.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartTrade.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartTrade.Fabrica;
using SmartTrade.Logica.Services;

namespace SmartTrade.ViewModels
{
    public class CatalogoVendedorViewModel : BaseViewModel
    {
        private STService service;

        public CatalogoVendedorViewModel(STService service)
        {
            this.service = service;
        }
    }
}
