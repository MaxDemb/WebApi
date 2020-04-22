using System.Data.Entity;
using WebApi.DAL.Entities;

namespace WebApi.DAL
{


    public class DbShopContext: DbContext
    {
        // Контекст настроен для использования строки подключения "DbShopContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "EntityProj.DbShopContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "DbShopContext" 
        // в файле конфигурации приложения.
        public DbShopContext()
            : base("name=DbShopContext")
        {
        }
        static DbShopContext()
        {
            Database.SetInitializer<DbShopContext>(new dbShopContextInitializer());
        }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }



    }
}