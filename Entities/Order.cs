using ToytoyStoreBackend.Entities.Base;
namespace ToytoyStoreBackend.Entities
{
    public class Order : Thing

    {
        public  DateTime OrderDate { get; set; } = DateTime.Now;
        public required string Status { get; set; } = "Pending"; // Pending, Shipped, Delivered
        public required int MemberId { get; set; }
        public required Member Member { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public required Payment Payment { get; set; } 
        public required Shipping Shipping { get; set; } 
    }
}