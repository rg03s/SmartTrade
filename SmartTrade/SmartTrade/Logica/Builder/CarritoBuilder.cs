using SmartTrade.Entities;
using SmartTrade.Logica.CarritoBuilder;
using SmartTrade.Logica.Services;
using SmartTrade.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CarritoBuilder : IBuilder
{
    private STService service;
    private List<ItemCarrito> carrito;
    private double costeTotal;
    private double puntosObtenidos;

    public CarritoBuilder()
    {
        this.service = STService.Instance;
        this.carrito = new List<ItemCarrito>();
        this.costeTotal = 0;
        this.puntosObtenidos = 0;
    }

    public void Reset()
    {
        this.carrito = new List<ItemCarrito>();
        this.costeTotal = 0;
        this.puntosObtenidos = 0;
    }

    public async Task SetProducts(List<ItemCarrito> carrito)
    {
        this.carrito = carrito;
        this.costeTotal = 0;
        this.puntosObtenidos = 0;

        foreach (var item in this.carrito)
        {
            Producto producto = await service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
            Producto_vendedor productoVendedor = producto.Producto_Vendedor.FirstOrDefault(pv => pv.Id == item.idProductoVendedor);

            this.costeTotal += productoVendedor.Precio * item.Cantidad;
            this.puntosObtenidos += producto.Puntos * item.Cantidad;
        }
    }

    public async Task AddItem(ItemCarrito item)
    {
        this.carrito.Add(item);

        Producto producto = await service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
        Producto_vendedor productoVendedor = producto.Producto_Vendedor.FirstOrDefault(pv => pv.Id == item.idProductoVendedor);

        this.costeTotal += productoVendedor.Precio * item.Cantidad;
        this.puntosObtenidos += producto.Puntos * item.Cantidad;
    }

    public async Task RemoveItem(ItemCarrito item)
    {
        this.carrito.Remove(item);

        Producto producto = await service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
        Producto_vendedor productoVendedor = producto.Producto_Vendedor.FirstOrDefault(pv => pv.Id == item.idProductoVendedor);

        this.costeTotal -= productoVendedor.Precio * item.Cantidad;
        this.puntosObtenidos -= producto.Puntos * item.Cantidad;
    }

    public double GetCosteTotal()
    {
        return this.costeTotal;
    }

    public double GetPuntosObtenidos()
    {
        return this.puntosObtenidos;
    }

    public List<ItemCarrito> GetProductos()
    {
        return this.carrito;
    }

    public async Task ActualizarCantidadItem(ItemCarrito item, int variacion)
    {

        if (item.Cantidad + variacion <= 0)
        {
            return;
        }

        Producto producto = await service.GetProductoByIdProductoVendedor(item.idProductoVendedor);
        Producto_vendedor productoVendedor = producto.Producto_Vendedor.FirstOrDefault(pv => pv.Id == item.idProductoVendedor);

        item.Cantidad += variacion;
        _ = service.ActualizarItemCarrito(item);

        this.costeTotal += productoVendedor.Precio * variacion;
        this.puntosObtenidos += producto.Puntos * variacion;

    }

}