using SmartTrade.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels
{
    public class CatalogoViewModel : BaseViewModel
    { 
        public IEnumerable<Producto> ProductosDestacados = [new Producto("amongus", "50%", new Image(), "asf")];
        public IEnumerable<Producto> CatalogoProductos = new List<Producto>();

        public CatalogoViewModel()
        {
        }
    }
}
