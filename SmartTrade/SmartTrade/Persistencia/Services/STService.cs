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
            dalUsuario.Add(u);
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

    }
}
