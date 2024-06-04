using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Logica.Estrategia
{
    public class PagoPaypal : IEstrategiaPago
    {

        private string correo;
        private string contraseña;
        public PagoPaypal(string correo, string contraseña)
        {
            this.correo = correo;
            this.contraseña = contraseña;
        }
        public void pagar()
        {
            UserDialogs.Instance.Toast("El pago con Paypal se ha realizado correctamente", TimeSpan.FromSeconds(3));
        }
    }
}
