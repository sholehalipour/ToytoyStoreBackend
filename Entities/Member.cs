using Microsoft.AspNetCore.Identity;
using toytoy_store_backend.Entities.Base;

namespace toytoy_store_backend.Entities
{
    public class Member : Thing
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public string? Email { get; set; }
        public int PhoneNumber { get; set; }
        public required String Password { get; set; }

    }
}