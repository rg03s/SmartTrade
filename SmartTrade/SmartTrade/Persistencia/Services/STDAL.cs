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
using SkiaSharp;
using Supabase.Interfaces;


namespace SmartTrade.Persistencia.Services
{
    public partial class STDAL<T> : IDAL<T> where T : class
    {

        private readonly SupabaseContext sc;
        protected readonly DbSet<T> table;


        public STDAL(SupabaseContext dbContext)
        {
            sc = dbContext;
            table = dbContext.Set<T>();
        }


        /*
        Future<List<Map<String, dynamic>>> getWhere<T>(
      String tableName, String columnName, dynamic value) async {
    final response = await _client
        .from(tableName)
        .select<List<Map<String, dynamic>>>()
        .eq(columnName, value);
    return response;
  }
        */
        /*
    public IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return sc.Set<T>().Where(predicate).AsEnumerable();
        }
        */
        public Usuario GetByEmail(string email)
        {
            if (sc.Usuario.Any(us => us.Email == email) == false) { return null; }
            else { return sc.Usuario.Where(us => us.Email == email).Single(); }
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            try {
                return await table.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public async Task Add(T entity)
        {
            await table.AddAsync(entity);
            await sc.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            table.Update(entity);
            await sc.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            table.Remove(entity);
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
