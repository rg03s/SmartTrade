using SmartTrade.Entities;
using SmartTrade.Logica.CarritoBuilder;
using SmartTrade.Logica.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public class Director    
{

    private STService service;

    public Director()
    {
        this.service = STService.Instance;
    }

    public async Task ConstruirCarrito(IBuilder builder)
    {
        builder.Reset();
        List<ItemCarrito> carrito = await service.GetCarrito();
        Console.WriteLine("Setting products... " + carrito.Count);
        builder.SetProducts(carrito);
    }
}
