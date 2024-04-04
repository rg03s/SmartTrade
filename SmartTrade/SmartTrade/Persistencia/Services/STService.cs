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
    public partial class STService
    {
        private readonly IDAL dal;

        public STService(IDAL dal)
        {
            this.dal = dal;
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

    }
}
