using System;
using WebApi.DAL.Entities;

namespace WebApi.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<Categories> Categories { get; }

        IRepository<Products> Products { get; }

        IRepository<Suppliers> Suppliers { get; }

        void Save();

    }
}
