using toytoy_store_backend.Entities.Base;

namespace toytoy_store_backend.Entities
{
    public class Orders : Thing

    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public double TotalAmount { get; set; }
        public required string PaymentMethod { get; set; }
    }
}