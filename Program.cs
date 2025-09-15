using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.DTOs.Common;
using ToytoyStoreBackend.DTOs.Members;
using ToytoyStoreBackend.DTOs.Products;
using ToytoyStoreBackend.Endpoints;
using ToytoyStoreBackend.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<LibraryDB>();
builder.Services.AddAuthentication();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
            };
        });

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthentication();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors(policy =>
{
    policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});
app.UseHttpsRedirection();
app.MapMemberEndPoints();
app.MapProductEndPoints();
app.Run();

