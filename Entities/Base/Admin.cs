namespace ToytoyStoreBackend.Entities.Base
{
    public class Admin : Thing
    {
        public required string UserName { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
    }
}