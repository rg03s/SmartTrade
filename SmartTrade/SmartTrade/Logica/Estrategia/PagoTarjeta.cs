using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Estrategia
{
    public class PagoTarjeta : IEstrategiaPago
    {

        private int numero;
        private DateTime fecha_cad;
        private int codSeduridad;
        public PagoTarjeta(int numeroTarjeta, DateTime fechaExpiracion, int codigoSeguridad)
        {
            this.numero = numeroTarjeta;
            this.fecha_cad = fechaExpiracion;
            this.codSeduridad = codigoSeguridad;
        }
        public void pagar()
        {
            UserDialogs.Instance.Toast("El pago con tarjeta se ha realizado correctamente", TimeSpan.FromSeconds(3));
        }
    }
}


