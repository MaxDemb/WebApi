namespace WebApi.DAL.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public Categories Category { get; set; }

        public Suppliers Supplier { get; set; }

    }
}
