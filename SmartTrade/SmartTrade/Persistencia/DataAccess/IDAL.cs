using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Persistencia.DataAccess
{
    public interface IDAL
    {
        Task Add<T>(T entity) where T : class;
        Task Delete<T>(T entity) where T : class;

        Task<List<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>(IComparable id) where T : class;
        Usuario GetByEmail(string email);

        Task Update<T>(T entity) where T : class;
        Task<List<int>> GetProductosIdLista(string nickPropietario);
        Task<List<ListaDeseosItem>> GetListaDeseos(string nickPropietario);
        //bool Exists<T>(IComparable id) where T : class;
        //void Clear<T>() where T : class;

        // void Commit();
    }
}