using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Postgrest;


namespace SmartTrade.Persistencia.Services
{
    public partial class STDAL<T> : IDAL<T> where T : class
    {

        private readonly SupabaseContext sc;
        protected readonly DbSet<T> _table;


        public STDAL(SupabaseContext dbContext)
        {
            sc = dbContext;
            _table = dbContext.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _table.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _table.AddAsync(entity);
            await sc.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _table.Update(entity);
            await sc.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _table.Remove(entity);
            await sc.SaveChangesAsync();
        }


        /*
        public async Task<bool> ExistsAsync<T>(IComparable id) 
        {
            return await Dbtabla.FindAsync(id) != null;
        }

        public void Clear<T>() where T : class
        {
            dbContext.Set<T>().RemoveRange(dbContext.Set<T>());
        }

        public IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbContext.Set<T>().Where(predicate).AsEnumerable();
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
        */

    }
}
