using ToytoyStoreBackend.Entities.Base;
namespace ToytoyStoreBackend.Entities
{
    public class Member : Thing
    {
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string UserName { get; set; }
        public required String Password { get; set; }

    }
}