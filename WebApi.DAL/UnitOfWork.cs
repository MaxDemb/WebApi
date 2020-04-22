using System;
using System.Linq;
using WebApi.DAL.Entities;
using WebApi.DAL.Interfaces;
using WebApi.DAL.Repositories;

namespace WebApi.DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private DbShopContext dbShopContext;
        private CategoriesRepository _categories;
        private ProductsRepository _products;
        private SuppliersRepository _suppliers;
        private bool disposed = false;


        public UnitOfWork()
        {
            dbShopContext = new DbShopContext();
            dbShopContext.Suppliers.ToList();
            dbShopContext.Products.ToList();
            dbShopContext.Categories.ToList();
        }


        public IRepository<Categories> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoriesRepository(dbShopContext);
                }
                return _categories;
            }
        }

        public IRepository<Products> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new ProductsRepository(dbShopContext);
                }
                return _products;
            }
        }

        public IRepository<Suppliers> Suppliers
        {
            get
            {
                if (_suppliers == null)
                {
                    _suppliers = new SuppliersRepository(dbShopContext);
                }
                return _suppliers;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
            {
                return;
            }

            if(disposing)
            {
                this.dbShopContext.Dispose();
            }

            disposed = true;
        }
        public void Save()
        {
            dbShopContext.SaveChanges();
        }
    }
}
