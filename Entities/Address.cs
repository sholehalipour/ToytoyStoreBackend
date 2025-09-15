namespace ToytoyStoreBackend.Entities.Base
{
    public class Address : Thing
    {
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
        public Member Member { get; set; } = null!;
    }
}