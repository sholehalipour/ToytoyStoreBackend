namespace ToytoyStoreBackend.DTOs.Members
{
    public class MemberListDto
    {

        public string? Id { get; set; }
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string UserName { get; set; }

    }
}