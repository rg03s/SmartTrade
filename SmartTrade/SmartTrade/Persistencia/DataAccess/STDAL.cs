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
            try
            {
                // Verificar si la entidad no es null
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad a eliminar no puede ser null");
                }

                // Verificar si la entidad existe en el contexto
                if (!sc.Set<T>().Local.Contains(entity))
                {
                    sc.Set<T>().Attach(entity);
                }

                // Eliminar la entidad
                sc.Set<T>().Remove(entity);
                await sc.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Manejar errores específicos de la base de datos
                string errorMessage = $"Error al intentar eliminar la entidad en la base de datos: {dbEx.Message}";
                if (dbEx.InnerException != null)
                {
                    errorMessage += $" | Inner Exception: {dbEx.InnerException.Message}";
                }
                Console.WriteLine(errorMessage); // Usar el mecanismo de logging apropiado
                throw new DataAccessException(errorMessage, dbEx);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de error
                string errorMessage = $"Error inesperado al intentar eliminar la entidad: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner Exception: {ex.InnerException.Message}";
                }
                Console.WriteLine(errorMessage); // Usar el mecanismo de logging apropiado
                throw new DataAccessException(errorMessage, ex);
            }
        }
        public void Commit()
        {
            sc.SaveChanges();
        }
    }
}
