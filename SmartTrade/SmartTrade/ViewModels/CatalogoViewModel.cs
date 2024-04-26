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

            //Inicializar las categorías disponibles
            Categorias = new List<string>
            {
                "Deportes",
                "Tecnología",
                "Papelería",
                "Ropa"
            };
        }

        /*public ObservableCollection<string> Categorias { get; set; } = new ObservableCollection<string>
        {
            "Deportes",
            "Tecnología",
            "Papelería",
            "Ropa"
        };*/

        // Propiedad para la categoría seleccionada
        //public string CategoriaSeleccionada { get; set; }

        private string categoriaSeleccionada;
        public string CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set
            {
                SetProperty(ref categoriaSeleccionada, value);
                // Aquí puedes agregar la lógica para filtrar los productos por la categoría seleccionada
                // Por ejemplo, puedes llamar a un método que actualice la lista de productos basada en la categoría seleccionada
                ActualizarProductosPorCategoria(value);
            }
        }

        private void ActualizarProductosPorCategoria(string categoria)
        {
            // Aquí puedes implementar la lógica para filtrar los productos por la categoría seleccionada
            // Por ejemplo, puedes recorrer CatalogoProductos y seleccionar solo los productos que pertenecen a la categoría seleccionada
            // Luego puedes actualizar la lista ProductosDestacados o cualquier otra lista que utilices en tu vista.
        }
    }
}
