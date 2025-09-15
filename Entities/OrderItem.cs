using ToytoyStoreBackend.Entities.Base;
namespace ToytoyStoreBackend.Entities
{
    public class OrderItem : Thing
    {

        public required int Quantity { get; set; }// تعداد محصول در سفارش
        public required decimal UnitPrice { get; set; }
        public required int OrderId { get; set; }
        public required Order Order { get; set; } = null!;
        public required int ProductId { get; set; }
        public required Product Product { get; set; } = null!;
    }
}