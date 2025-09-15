using ToytoyStoreBackend.Entities;

namespace ToytoyStoreBackend.DTOs.Products
{
    public class ProductListDto
    {
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public  Category? Category { get; set; }
        public string? Brand { get; set; }
        public string? Sku { get; set; }
        public double? Price { get; set; }
    }
}
