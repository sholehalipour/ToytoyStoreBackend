using System;

namespace ToytoyStoreBackend.DTOs.Auth;

public class LoginResultDto
{
    public bool Successfull { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; }
}
