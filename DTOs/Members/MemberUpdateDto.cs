namespace ToytoyStoreBackend.DTOs.Members
{
    public class MemberUpdateDto
    {
        public string? Name { get; set; }
        public required string Family { get; set; }
        public required string UserName { get; set; }

    }
}