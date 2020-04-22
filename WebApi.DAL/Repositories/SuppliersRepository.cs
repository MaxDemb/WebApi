using System.Collections.Generic;
using WebApi.DAL.Entities;
using System.Linq;
using WebApi.DAL.Interfaces;
using System;
using System.Data.Entity.Core;

namespace WebApi.DAL.Repositories
{
    public class SuppliersRepository : IRepository<Suppliers>
    {

        private DbShopContext dbShopContext;

        public SuppliersRepository(DbShopContext dbShopContext)
        {
            this.dbShopContext = dbShopContext;
        }

        public void Create(Suppliers item)
        {
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentException("Name can not be null!");
            }
            item.Id = this.getMaxId() + 1;
            
            dbShopContext.Suppliers.Add(item);
            dbShopContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplier = dbShopContext.Suppliers.Find(id);
            if (supplier == null)
            {
                throw new ObjectNotFoundException();
            }
            else 
            {
                dbShopContext.Suppliers.Remove(supplier);
                dbShopContext.SaveChanges();
            }
        }

        public Suppliers Get(int id)
        {
            return dbShopContext.Suppliers.Find(id);
        }

        public IEnumerable<Suppliers> GetAll()
        {
            return dbShopContext.Suppliers;
        }

        public void Update(Suppliers item)
        {

                dbShopContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbShopContext.SaveChanges();

        }


        private int getMaxId()
        {
            IEnumerable<Suppliers> suppliers = this.GetAll();
            return suppliers.Max(x => x.Id);
             
        }
    }
}
