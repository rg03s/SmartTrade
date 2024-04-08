using Microsoft.EntityFrameworkCore;
using SmartTrade.Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Persistencia.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        protected readonly SupabasePrueba sc;
        protected readonly DbSet<T> _table;
        public RepositorioGenerico(SupabasePrueba context)
        {
            sc = context;
            _table = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetById(int id)
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
    }
}
