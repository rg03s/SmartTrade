using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartTrade.Persistencia.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioGenerico<Usuario>
    {
        void AñadirUsuario(Usuario u);
    }
}
