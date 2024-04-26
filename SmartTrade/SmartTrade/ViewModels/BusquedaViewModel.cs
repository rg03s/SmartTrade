using SmartTrade.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartTrade.Entities;
using Microsoft.EntityFrameworkCore;
using SmartTrade.Logica.Services;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartTrade.Fabrica;
using System.ComponentModel;

namespace SmartTrade.ViewModels
{
    public class BusquedaViewModel : BaseViewModel
    {
        private STService service;

        public string busqueda { get; set; }
        public List<Producto> productosBuscados { get; set; }

        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Huella_eco { get; set; } 

        public BusquedaViewModel(STService service, string busqueda, List<Producto> productosB) 
        {
            this.service = service;
            this.productosBuscados = productosB;
            this.busqueda = busqueda;
        }

        public ObservableCollection<string> Categorias { get; set; } = new ObservableCollection<string>
        {
            "Deportes",
            "Tecnología",
            "Papelería",
            "Ropa"
        };

        // Propiedad para la categoría seleccionada
        public string CategoriaSeleccionada { get; set; }
    }
}
