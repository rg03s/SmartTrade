using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Observador
{
    public class Alerta
    {
        private List<IObservador> _observadores = new List<IObservador>();

        public void AgregarObservador(IObservador observador)
        {
            _observadores.Add(observador);
        }

        public void EliminarObservador(IObservador observador)
        {
            _observadores.Remove(observador);
        }

        public void Notificar()
        {
            foreach (var observador in _observadores)
            {
                observador.Actualizar();
            }
        }
    }
}
