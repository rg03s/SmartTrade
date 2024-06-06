using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Estrategia
{
    public class PagoTarjeta : IEstrategiaPago
    {

        private string numero;
        private DateTime fecha_cad;
        private int codSeduridad;
        public PagoTarjeta(string numeroTarjeta, DateTime fechaExpiracion, int codigoSeguridad)
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


