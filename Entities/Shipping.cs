using ToytoyStoreBackend.Entities.Base;

namespace ToytoyStoreBackend.Entities
{
    public class Shipping : Thing
    {
        public required string Method { get; set; } = "Post"; // Post, Courier
        public string? TrackingCode { get; set; }
        public required string Status { get; set; } = "Processing"; // Processing, Shipped, Delivered
        public required int OrderId { get; set; }
        public required Order Order { get; set; } 
    }
}