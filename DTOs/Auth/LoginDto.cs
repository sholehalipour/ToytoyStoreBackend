using System;

namespace ToytoyStoreBackend.DTOs.Auth;

public class LoginDto
{
    public required string UserName { get; set; }
    public string? Password { get; set; }
}
