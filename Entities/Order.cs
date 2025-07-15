using ToytoyStoreBackend.Entities.Base;
namespace ToytoyStoreBackend.Entities
{
    public class Order : Thing

    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public double TotalAmount { get; set; }
        public required string PaymentMethod { get; set; }
    }
}