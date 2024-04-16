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
        public ObservableCollection<string> ImagenDestacados { get; set; }
        public ObservableCollection<string> NombreDestacados { get; set; }
        public ObservableCollection<string> HuellaDestacados { get; set; }
        public string NombreDestacado1 { get; set; }

        public CatalogoViewModel(STService service) 
        {
            this.service = service;
            try
            {
                ProductosDestacados = new ObservableCollection<Producto>();
                CatalogoProductos = new ObservableCollection<Producto>();
                foreach (Producto pd in service.GetProductosDestacadosAsync().Result)
                {
                    ProductosDestacados.Add(pd);
                    ImagenDestacados.Add(pd.Imagen);
                    NombreDestacados.Add(pd.Nombre);
                    HuellaDestacados.Add(pd.Huella_eco);
                }
                foreach (Producto p in service.GetAllProductsAsync().Result)
                {
                    CatalogoProductos.Add(p);
                }
            } catch (Exception ex) { }
        }
    }
}
