using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Entities
{
    public partial class Tecnologia : Producto
    {
        public string Dispositivo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
    }
}
