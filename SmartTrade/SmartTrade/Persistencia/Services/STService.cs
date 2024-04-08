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
    public class STService : ISTService
    {
        private readonly IDAL<Usuario> dalUsuario;
        private SupabaseContext supabaseContext = SupabaseContext.Instance;
        private static STService instance = new STService();
        private Usuario loggedUser;

        public STService()
        {
            dalUsuario = new STDAL<Usuario>(supabaseContext);

        }
        public static STService Instance
        {
            get
            {
                return STService.instance;
            }
        }

        public void AddUser(Usuario u)
        {
        {
        }





        /*
        public void Commit()
        {
            dal.Commit();
        }

        public void AddUser(Usuario usuario)
        {
           
            
                dal.Insert<Usuario>(usuario);
                dal.Commit();
                

            
        }

        public void GetUsuarios() {
            dal.GetAll<Usuario>();
        }
        */

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
