using toytoy_store_backend.Entities.Base;

namespace toytoy_store_backend.Entities
{
    public class Product : Thing
    {
        public required string Productname { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required string Brand { get; set; }
        public required string Sku { get; set; }
    }
}