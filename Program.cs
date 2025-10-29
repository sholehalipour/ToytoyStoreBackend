using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.DTOs.Auth;
using ToytoyStoreBackend.DTOs.Common;
using ToytoyStoreBackend.DTOs.Members;
using ToytoyStoreBackend.DTOs.Products;
using ToytoyStoreBackend.Endpoints;
using ToytoyStoreBackend.Entities;
using ToytoyStoreBackend.Entities.Base;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<LibraryDB>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ToyToyStoryBackend.ir",
            ValidAudience = "ToyToyStoryBackend.ir",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJndWlkIjoiYmQ0ZGQwNzgtNWUyNi00NmFkLThiYjQtNWE2Mzk0MzliYjU3IiwiZnVsbG5hbWUiOiLZhdiv24zYsduM2Kog2LPYp9mF2KfZhtmHIiwiZXhwIjoxNzY1NjkxOTk1LCJpc3MiOiJUb3lUb3lTdG9yeUJhY2tlbmQuaXIiLCJhdWQiOiJUb3lUb3lTdG9yeUJhY2tlbmQuaXIifQ.5ZoZMLPbrMPLu3Ds1LNFCLcuoLMYhzHp0CgyTcouMR0"))
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors(policy =>
{
    policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});
app.MapPost("auth/login", async Task<LoginResultDto> ([FromServices] LibraryDB db, [FromBody] LoginDto dto) =>
{
    if (!await db.Admins.AnyAsync())
    {
        await db.Admins.AddAsync(new Admin
        {
            UserName = "admin",
            Password = "admin",
            Fullname = "مدیریت سامانه",
        });
        await db.SaveChangesAsync();
    }
    var admin = await db.Admins.FirstOrDefaultAsync(m => m.UserName == dto.UserName && m.Password == dto.Password);
    if (admin != null)
    {
        var claims = new[]
            {
            new Claim("guid",admin.Guid),
            new Claim("fullname",admin.Fullname??"بی نام")
                      // new Claim(JwtRegisteredClaimNames.Sub, username),
                   // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJndWlkIjoiYmQ0ZGQwNzgtNWUyNi00NmFkLThiYjQtNWE2Mzk0MzliYjU3IiwiZnVsbG5hbWUiOiLZhdiv24zYsduM2Kog2LPYp9mF2KfZhtmHIiwiZXhwIjoxNzY1NjkxOTk1LCJpc3MiOiJUb3lUb3lTdG9yeUJhY2tlbmQuaXIiLCJhdWQiOiJUb3lUb3lTdG9yeUJhY2tlbmQuaXIifQ.5ZoZMLPbrMPLu3Ds1LNFCLcuoLMYhzHp0CgyTcouMR0"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "ToyToyStoryBackend.ir",
            audience: "ToyToyStoryBackend.ir",
            claims: claims,
            expires: DateTime.Now.AddMonths(2),
            signingCredentials: creds);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return new LoginResultDto
        {
            Successfull = true,
            Token = tokenString,
        };
    }
    return new LoginResultDto
    {
        Successfull = false,
        Message = "نام کاربری یا کلمه عبور صحیح نیست",
    };
});
app.MapProductEndPoints();
app.MapMemberEndPoints();
app.Run();

