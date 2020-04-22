using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using WebApi.DAL.Entities;
using WebApi.DAL.Interfaces;

namespace WebApi.DAL.Repositories
{
    public class CategoriesRepository : IRepository<Categories>
    {
        private DbShopContext dbShopContext;

        public CategoriesRepository(DbShopContext dbShopContext)
        {
            this.dbShopContext = dbShopContext;
        }

        public void Create(Categories item)
        {
            if (String.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentException("Name can not be null!");
            }
            item.Id = this.getMaxId() + 1;

            dbShopContext.Categories.Add(item);
            dbShopContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = dbShopContext.Categories.Find(id);
            if (category == null)
            {
                throw new ObjectNotFoundException();
            }
            else
            {
                dbShopContext.Categories.Remove(category);
                dbShopContext.SaveChanges();
            }
        }

        public Categories Get(int id)
        {
            return dbShopContext.Categories.Find(id);
        }

        public IEnumerable<Categories> GetAll()
        {
            return dbShopContext.Categories;
        }

        public void Update(Categories item)
        {
            dbShopContext.Entry(item).State = EntityState.Modified;
        }


        private int getMaxId()
        {
            IEnumerable<Categories> categories = this.GetAll();
            return categories.Max(x => x.Id);

        }
    }
}
