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

        public ObservableCollection<Producto> ProductosDestacados { get; set; }
        public ObservableCollection<Producto> CatalogoProductos { get; set; }

        public CatalogoViewModel(STService service) 
        {
            this.service = service;
            try
            {
                ProductosDestacados = new ObservableCollection<Producto>();
                CatalogoProductos = new ObservableCollection<Producto>();

                Ropa p1 = new Ropa("Camiseta Valencia CF", "30%", "https://i.ibb.co/d7vYJM6/Camiseta-Valencia.jpg", "",
                                        "Camiseta del Valencia CF en muy buen estado de segunda mano.\n\nTalla M.", 10, new Categoria("Ropa") , "M",
                                            "Blanca", "Deporte", "Camiseta");

                Deporte p2 = new Deporte("Pelota Baloncesto", "50%", "https://i.ibb.co/LNSNFFf/Pelota-Baloncesto.png", "",
                                            "Pelota de Baloncesto de mi hijo. Le gustaba mucho pero se murió. La vendo barata.", 20, new Categoria("Deporte"), "Pelota");

                Papeleria p3 = new Papeleria("Cuaderno de colores", "75%", "https://i.ibb.co/qkQKMpc/Cuaderno-Colores.png", "",
                                                "Cuadernos muy bonitos del color que elijas. Muy buena calidad.", 20, new Categoria("Papeleria"), "Plástico");

                Tecnologia p4 = new Tecnologia("GameBoy Color", "20%", "https://i.ibb.co/sC5pJzS/GBC.png", "",
                                                "GameBoy Color muy antigua. Funciona más o menos pero un pokemon te echas tranquilamente.", 1, new Categoria("Tecnologia"),
                                                      "Consola", "Nintendo", "GameBoy Color");

                p1.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(10, "ValenciaFan", 10, 79.99) };
                p2.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(11, "Lebron James", 1, 4.99) };
                p3.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(12, "PickMeGirl", 50, 12.99) };
                p4.Producto_Vendedor = new List<Producto_vendedor> { new Producto_vendedor(13, "UltraNerd69", 1, 19.99) };

                CatalogoProductos.Add(p1);
                CatalogoProductos.Add(p2);
                CatalogoProductos.Add(p3);
                CatalogoProductos.Add(p4);
            } catch (Exception ex) { }
        }
    }
}
