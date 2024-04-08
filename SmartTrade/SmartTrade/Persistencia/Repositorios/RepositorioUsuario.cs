using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

//using Microsoft.EntityFrameworkCore.Storage;
using SkiaSharp;
using SmartTrade.Entities;
using SmartTrade.Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Transactions;

namespace SmartTrade.Persistencia.Repositorios
{
    internal class RepositorioUsuario : RepositorioGenerico<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(SupabasePrueba context) : base(context)
        {

        }
        public void AñadirUsuario(Usuario u)
        {
            
                try
                {
                    sc.Usuario.Add(u);
                    sc.SaveChanges();

                }
                catch (DbUpdateException ex)
                {
                    // Log the exception message and inner exception details for further investigation
                    Console.WriteLine("Error al guardar cambios en la base de datos:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Excepción interna:");
                    Console.WriteLine(ex.InnerException);
                    // También puedes intentar acceder a otras propiedades de la excepción interna para obtener más información si está disponible
                }
           
        }

        public void MostrarUsuarios() 
        {
            var usuarios = sc.Usuario.ToList();

            Console.WriteLine("Lista de Usuarios:");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"ID: {usuario.Nickname}, Nombre: {usuario.Nombre}");
                // Puedes mostrar otros campos de usuario aquí según sea necesario
            }
        }

    }
}
