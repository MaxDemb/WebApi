using System.Collections.Generic;


namespace WebApi.DAL.Entities
{
    public class Suppliers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public ICollection<Products> Products { get; set; }


    }
}
