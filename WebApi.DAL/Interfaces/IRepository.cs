﻿using System.Collections.Generic;

namespace WebApi.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
