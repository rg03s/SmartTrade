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

namespace SmartTrade.ViewModels
{
    public class CatalogoViewModel : BaseViewModel
    {
        private STService service;

        public ObservableCollection<Producto> ProductosDestacados { get; set; }
        public ObservableCollection<Producto> CatalogoProductos { get; set; }

        public CatalogoViewModel(STService service) 
        {
            this.service = service;
            try
            {
                foreach (Producto pd in service.GetProductosDestacados())
                {
                    ProductosDestacados.Add(pd);
                }
                foreach (Producto p in service.GetAllProducts())
                {
                    CatalogoProductos.Add(p);
                }
            } catch (Exception ex) { }
        }
    }
}
