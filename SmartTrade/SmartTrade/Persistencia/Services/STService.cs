using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;
using SmartTrade.Persistencia;

namespace SmartTrade.Persistencia.Services
{
    public partial class STService : ISTService
    {
        private readonly IDAL dal;
        private Usuario loggedUser;

        public STService(IDAL dal)
        {
            this.dal = dal;
            loggedUser = new Usuario();
        }

        public void Commit()
        {
            dal.Commit();
        }

        public void AddUser(Usuario usuario)
        {
           
            if (dal.GetById<Usuario>(usuario.Email) == null)
            {
                dal.Insert<Usuario>(usuario);
                Commit();

            }
        }

        public void GetUsuarios() {
            dal.GetAll<Usuario>();
        }

        public bool Login(string email, string password)
        {
            // Si no existe el usuario
            Usuario usuario = dal.GetById<Usuario>(email);

            if (usuario == null)
            {
                return false;
            }
            
            // Si la contraseña no coincide
            else if (usuario.Contraseña != password)
            {
                return false;
            }

            else
            {
                loggedUser = usuario;
                return true;
            }

        }

    }
}
