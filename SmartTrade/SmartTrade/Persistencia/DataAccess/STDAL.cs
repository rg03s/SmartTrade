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


namespace SmartTrade.Persistencia.DataAccess
{
    public partial class STDAL : IDAL
    {

        private readonly SupabaseContext sc;
        //protected readonly DbSet<T> table;


        public STDAL(SupabaseContext dbContext)
        {
            sc = dbContext;
        }

        public Usuario GetByEmail(string email)
        {
            if (sc.Usuario.Any(us => us.Email == email) == false) { return null; }
            else { return sc.Usuario.Where(us => us.Email == email).Single(); }
        }
        public async Task<List<T>> GetAll<T>() where T : class
        {
            try
            {
                return await sc.Set<T>().ToListAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<T> GetById<T>(IComparable id) where T : class
        {
            try
            {
                return await sc.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task Add<T>(T entity) where T : class
        {
            await sc.Set<T>().AddAsync(entity);
            await sc.SaveChangesAsync();
        }
        public async Task Update<T>(T entity) where T : class
        {
            sc.Set<T>().Update(entity);
            await sc.SaveChangesAsync();
        }
        public async Task Delete<T>(T entity) where T : class
        {
            sc.Set<T>().Remove(entity);
            await sc.SaveChangesAsync();
        }
        public void Commit()
        {
            sc.SaveChanges();
        }
    }
}
