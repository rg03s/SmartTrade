using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Observador
{
    public class ManagerAlertas
    {
        private List<IObservador> observadores = new List<IObservador>();

        public void AgregarObservador(IObservador observador)
        {
            observadores.Add(observador);
        }

        public void EliminarObservador(IObservador observador)
        {
            observadores.Remove(observador);
        }

        public void NotificarObservadores(Producto p)
        {
            foreach (IObservador observador in observadores)
            {
                observador.Actualizar(p);
            }
        }
    }
}
