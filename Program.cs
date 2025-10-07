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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"))
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();

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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
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
app.UseHttpsRedirection();
app.MapMemberEndPoints();
app.MapProductEndPoints();
app.Run();

