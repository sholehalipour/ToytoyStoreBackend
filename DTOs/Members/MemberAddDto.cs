namespace ToytoyStoreBackend.DTOs.Members
{
    public class MemberAddDto
    {

        public string? Name { get; set; }
        public required string Family { get; set; }
        public required string UserName { get; set; }
        public   required String Password { get; set; }


    }
}