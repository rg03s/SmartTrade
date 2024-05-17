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

        public async Task<List<ListaDeseosItem>> GetListaDeseos(string nickPropietario)
        {
            return await sc.ListaDeseosItem.Where(ld => ld.NickPropietario == nickPropietario).ToListAsync();

        }
        public async Task<List<int>> GetProductosIdLista(string nickPropietario)
        {
            try
            {
                List<ListaDeseosItem> listaDeseos = await GetListaDeseos(nickPropietario);
                List<int> productosId = new List<int>();
                foreach (ListaDeseosItem ld in listaDeseos)
                {
                    productosId.Add(ld.ProductoVendedorId);
                }
                return productosId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetProductosListaDeseosByUsuario: " + ex.Message);
                return new List<int>(); // Devolver una lista vacía en caso de error
            }
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
        */
        public void Commit()
        {
            sc.SaveChanges();
        }
        

    }
}
