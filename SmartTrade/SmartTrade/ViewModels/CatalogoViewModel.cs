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
    public class CatalogoViewModel : BaseViewModel
    {
        private STService service;

        public List<Producto> productosDestacados { get; set; }
        public List<Producto> catalogoProductos { get; set; }
        public List<string> Categorias { get; set; }

        public CatalogoViewModel(STService service, List<Producto> catalogoProductos)
        {
            this.service = service;
            this.catalogoProductos = catalogoProductos;
        }
    }
}
