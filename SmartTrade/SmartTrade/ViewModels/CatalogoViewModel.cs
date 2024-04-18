using SmartTrade.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartTrade.Entities;
using Microsoft.EntityFrameworkCore;
using SmartTrade.Persistencia.Services;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartTrade.Fabrica;

namespace SmartTrade.ViewModels
{
    public class CatalogoViewModel : BaseViewModel
    {
        private STService service;

        public List<Producto> productosDestacados { get; set; }
        public List<Producto> catalogoProductos { get; set; }

        public CatalogoViewModel(STService service, List<Producto> catalogoProductos) 
        {
            this.service = service;
            this.catalogoProductos = catalogoProductos;
        }
    }
}
