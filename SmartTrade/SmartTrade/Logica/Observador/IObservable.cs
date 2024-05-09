using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Observador
{
    public interface IObservable
    {
        void AddObservador(IObservador observador);
        void RemoveObservador(IObservador observador);
        void NotificarObservadores();
    }
}
