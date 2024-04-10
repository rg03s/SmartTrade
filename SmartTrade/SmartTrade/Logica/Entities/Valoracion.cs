using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Entities
{
    public partial class Valoracion
    {
        public Valoracion() { }
        public Valoracion(DateTime fecha, int nota, string comentario, string comprador, int producto)
        {
            this.Fecha = fecha;
            this.Nota = nota;
            this.Comentario = comentario;
            this.Comprador.Nickname = comprador;
            this.Producto.Id = producto;
        }
    }
}
