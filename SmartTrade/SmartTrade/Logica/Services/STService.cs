﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Postgrest;
using SmartTrade.Entities;
using SmartTrade.Persistencia.DataAccess;
using Supabase.Gotrue;
using Xamarin.Essentials;

namespace SmartTrade.Logica.Services
{
    public class STService : ISTService
    {
        private readonly IDAL<Usuario> dalUsuario;
        private readonly IDAL<Vendedor> dalVendedor;
        private readonly IDAL<Comprador> dalComprador;
        private readonly IDAL<Tarjeta> dalTarjeta;
        private readonly IDAL<Producto> dalProducto;
        private SupabaseContext supabaseContext = SupabaseContext.Instance;
        private static STService instance = new STService();
        private Usuario loggedUser;

        public STService()
        {
            dalUsuario = new STDAL<Usuario>(supabaseContext);
            dalProducto = new STDAL<Producto>(supabaseContext);
        }
        public static STService Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task AddUser(Usuario usuario)
        {
            if (dalUsuario.GetByEmail(usuario.Email) != null)
            {
                throw new EmailYaRegistradoException();
            }
            else if (await dalUsuario.GetById(usuario.Nickname) != null)
            {
                throw new NickYaRegistradoException();
            }
            else
            {
                if (usuario.Email.Contains("@"))
                {
                    if (usuario.Email.StartsWith("@"))
                        throw new EmailFormatoIncorrectoException();
                    if (usuario.Email.EndsWith(".com") || usuario.Email.EndsWith(".es"))
                    {
                        await dalUsuario.Add(usuario);
                    }
                    else throw new EmailFormatoIncorrectoException();
                }
                else throw new EmailFormatoIncorrectoException();
            }
        }

        public bool MayorDe18(DateTime fecha_nac)
        {
            TimeSpan edad = DateTime.Now - fecha_nac;
            if ((int)(edad.TotalDays / 365.25) >= 18) return true;
            else return false;
        }

        public async Task AddUserVendedor(Vendedor vendedor)
        {

            await dalVendedor.Add(vendedor);
        }

        public async Task AddUserComprador(Comprador comprador)
        {
            await dalComprador.Add(comprador);
        }

        public async Task AddTarjeta(Tarjeta tarjeta)
        {
            await dalTarjeta.Add(tarjeta);
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


        /*public async Task<bool> Login(string nickname, string password)
        {
            Usuario usuario = await dalUsuario.GetById(nickname);
            Usuario correo = dalUsuario.GetByEmail(usuario.Email);
            // Si no existe el usuario
            if (usuario == null || correo == null)
            {
                return false;
            }

            // Si la contraseña no coincide
            else if (usuario.Password != password)
            {
                return false;
            }

            else
            {
                loggedUser = usuario;
                return true;
            }

        }*/

        public async Task<bool> Login(string identifier, string password)
        {
            try
            {
                Usuario usuario = await dalUsuario.GetById(identifier);
                if (usuario == null)
                {
                    usuario = dalUsuario.GetByEmail(identifier);
                    if (usuario == null)
                    {
                        return false; // El usuario no está registrado ni por nickname ni por correo
                    }
                }

                if (usuario.Password != password)
                {
                    return false; // La contraseña no coincide
                }

                // Inicio de sesión exitoso
                loggedUser = usuario;
                return true;
            }
            catch (Exception)
            {
                // Manejar la excepción adecuadamente, por ejemplo, registrándola o notificando al usuario
                return false;
            }
        }

        public async Task<List<Producto>> GetAllProductsAsync()
        {
            var productos = await dalProducto.GetAll();
            return productos.ToList();
        }
    }
}