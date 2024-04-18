﻿using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Persistencia.DataAccess
{
    public interface IDAL<T> where T : class
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Usuario GetByEmail(string email);
        //bool Exists<T>(IComparable id) where T : class;
        //void Clear<T>() where T : class;

        // void Commit();
    }
}