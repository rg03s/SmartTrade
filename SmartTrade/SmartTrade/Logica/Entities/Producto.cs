using SmartTrade.Logica.Entities;
using SmartTrade.Logica.Observador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Producto : IObservable
    {
        public Producto() {
            this.Producto_Vendedor = new List<Producto_vendedor>();
            this.observadoresListaDeseos = new List<IObservador>();
        }

        public Producto(string nombre, string huella, string imagen, string modelo3d, string desc, int puntos, string cat)
        {
            this.Nombre = nombre;
            this.Huella_eco = huella;
            this.Imagen = imagen;
            this.Modelo3d = modelo3d;
            this.Descripcion = desc;
            this.Puntos = puntos;
            this.Categoria = cat;
            
        }

        public void AddObservador(IObservador observador)
        {
            observadoresListaDeseos.Add(observador);
        }

        public void RemoveObservador(IObservador observador)
        {
            observadoresListaDeseos.Remove(observador);
        }

        public void NotificarObservadores()
        {
            foreach (IObservador o in observadoresListaDeseos)
            {
                o.Actualizar(this);
            }
        }

        public void ReducirStock(Producto_vendedor pv, int cantidad)
        {
            try
            {
                if (pv.Stock >= cantidad)
                {
                    pv.Stock -= cantidad;

                    if (pv.Stock == 0)
                    {
                        NotificarObservadores();
                    }

                }
                else
                {
                  throw new Exception("No hay suficiente stock");
                }

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
