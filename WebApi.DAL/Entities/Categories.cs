using System.Collections.Generic;

namespace WebApi.DAL.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }

    }
}
