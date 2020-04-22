using System;
using System.Collections.Generic;
using WebApi.DAL.Entities;
using System.Data.Entity;

namespace WebApi.DAL
{
    public class dbShopContextInitializer : DropCreateDatabaseIfModelChanges<DbShopContext>
    {
        protected override void Seed(DbShopContext dbShopContext)
        {
            Categories cat1 = new Categories { Name = "Іграшки" };
            Categories cat2 = new Categories { Name = "Одяг" };
            Categories cat3 = new Categories { Name = "Електроніка" };
            Categories cat4 = new Categories { Name = "Побутова Техніка" };
            Categories cat5 = new Categories { Name = "Продукти" };
            Categories cat6 = new Categories { Name = "Книги" };
            dbShopContext.Categories.AddRange(new List<Categories> { cat1, cat2, cat3, cat4, cat5, cat6 });



            Suppliers sup1 = new Suppliers { Name = "Apple inc." };
            Suppliers sup2 = new Suppliers { Name = "SomeCompany" };
            Suppliers sup3 = new Suppliers { Name = "NoNameCO" };
            Suppliers sup4 = new Suppliers { Name = "Evill" };
            dbShopContext.Suppliers.AddRange(new List<Suppliers> { sup1, sup2, sup3, sup4 });


            Products prod1 = new Products { Name = "Iphone7", Category = cat3, Supplier = sup1, Price = 700 };
            Products prod2 = new Products { Name = "Samsung s6", Category = cat3, Supplier = sup2, Price = 500 };
            Products prod3 = new Products { Name = "Пралка BOSH", Category = cat4, Supplier = sup2, Price = 300 };
            Products prod4 = new Products { Name = "Дзига", Category = cat1, Supplier = sup3, Price = 5 };
            Products prod5 = new Products { Name = "йойо", Category = cat1, Supplier = sup4, Price = 2 };
            Products prod6 = new Products { Name = "яйця", Category = cat5, Supplier = sup4, Price = 0.1M };
            Products prod7 = new Products { Name = "сир", Category = cat5, Supplier = sup1, Price = 10 };
            Products prod8 = new Products { Name = "молоко", Category = cat5, Supplier = sup2, Price = 1 };
            Products prod9 = new Products { Name = "гречка", Category = cat5, Supplier = sup3, Price = 1 };
            Products prod10 = new Products { Name = "Iphone 8", Category = cat3, Supplier = sup1, Price = 800 };
            Products prod11 = new Products { Name = "Asus Notebook", Category = cat3, Supplier = sup4, Price = 500 };
            dbShopContext.Products.AddRange(new List<Products> { prod1, prod2, prod3, prod4, prod5, prod6, prod7, prod8, prod9, prod10, prod11 });


        }
        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}
