using System.Collections.Generic;
using WebApi.DAL.Interfaces;
using WebApi.DAL.Entities;
using System;
using System.Linq;
using System.Data.Entity.Core;

namespace WebApi.DAL.Repositories
{
    public class ProductsRepository : IRepository<Products>
    {


        DbShopContext dbShopContext;

        public ProductsRepository(DbShopContext dbShopContext)
        {
            this.dbShopContext = dbShopContext;
        }
        public void Create(Products item)
        {
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentException("Name can not be null!");
            }
            item.Id = this.getMaxId() + 1;

            dbShopContext.Products.Add(item);
            dbShopContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var products = dbShopContext.Products.Find(id);
            if (products == null)
            {
                throw new ObjectNotFoundException();
            }
            else
            {
                dbShopContext.Products.Remove(products);
                dbShopContext.SaveChanges();
            }
        }

        public Products Get(int id)
        {
            return dbShopContext.Products.Find(id);
        }

        public IEnumerable<Products> GetAll()
        {
            return dbShopContext.Products;
        }

        public void Update(Products item)
        {
            dbShopContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
            dbShopContext.SaveChanges();
        }


        private int getMaxId()
        {
            IEnumerable<Products> products = this.GetAll();
            return products.Max(x => x.Id);

        }
    }
}
