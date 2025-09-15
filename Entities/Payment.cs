namespace ToytoyStoreBackend.Entities.Base
{
    public class Payment : Thing
    {
        public required string Method { get; set; } = "Online"; // Online, CashOnDelivery
        public required string Status { get; set; } = "Pending"; // Pending, Paid
        public required DateTime PaymentDate { get; set; } = DateTime.Now;
        public required int OrderId { get; set; }
        public required Order Order { get; set; } = null!;
    }
}