using ToytoyStoreBackend.Entities.Base;

namespace ToytoyStoreBackend.Entities
{
    public class Category : Thing
    {
        public  string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}