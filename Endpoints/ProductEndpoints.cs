using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.DTOs.Common;
using ToytoyStoreBackend.DTOs.Products;
using ToytoyStoreBackend.Entities;
using ToytoyStoreBackend.Entities.Base;

namespace ToytoyStoreBackend.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndPoints(this WebApplication app)
        {
            app.MapGet("products/list", async
            ([FromServices] LibraryDB db,
             HttpContext context) =>
{
    var user = context.User;
    var adminGuid = user.Claims.FirstOrDefault(m => m.Type == "guid")?.Value;
    var admin = await db.Admins.FirstOrDefaultAsync(m => m.Guid == adminGuid);
    var result = await db.Products.Where(m => m.Owner == admin).Select(b => new ProductListDto
    {
        Id = b.Guid,
        ProductName = b.ProductName,
        Description = b.Description,
        Brand = b.Brand,
        Category = b.Category,
        Sku = b.Sku,
        Price = b.Price,
    }).ToListAsync();
    return result;
}).RequireAuthorization();
            app.MapPost("products/create", async
           ([FromServices] LibraryDB db,
           HttpContext context,
           [FromBody] ProductAddDto ProductAddDto) =>
        {
            var user = context.User;
            var adminGuid = user.Claims.FirstOrDefault(m => m.Type == "guid")?.Value;
            var admin = await db.Admins.FirstOrDefaultAsync(m => m.Guid == adminGuid);
            if (admin != null)
            {
                var product = new Product

                {
                    ProductName = !string.IsNullOrEmpty(ProductAddDto.ProductName) ? ProductAddDto.ProductName : " بدون نام محصول",
                    Description = !string.IsNullOrEmpty(ProductAddDto.Description) ? ProductAddDto.Description : " بدون توضیح",   // Category = !string.IsNullOrEmpty(ProductAddDto.Category.Name) ? ProductAddDto.Category.Name : "بدون دسته بندی",
                    Brand = !string.IsNullOrEmpty(ProductAddDto.Brand) ? ProductAddDto.Brand : "بدون برند",
                    Sku = !string.IsNullOrEmpty(ProductAddDto.Sku) ? ProductAddDto.Sku : "بدون شماره سریال",
                    Price = ProductAddDto.Price ?? 0,
                    Owner = admin,
                };
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Message = "Products Created!"
                };
            }
            return new CommandResultDto
            {
                Successfull = false,
                Message = "Unknown User"
            };

        }).RequireAuthorization();
            app.MapPut("products/update/{guid}",
     async ([FromServices] LibraryDB db,
     [FromRoute] string guid,
     [FromBody] ProductUpdateDto productUpdateDto) =>
    {
        var b = await db.Products.FirstOrDefaultAsync(m => m.Guid == guid);
        if (b == null)
        {
            return new CommandResultDto
            {
                Successfull = false,
                Message = "Not Found Product!"
            };
        }
        b.ProductName = productUpdateDto.ProductName ?? "";
        b.Description = productUpdateDto.Description ?? "";
        b.Brand = productUpdateDto.Brand ?? "";
        b.Category = productUpdateDto.Category;
        b.Sku = productUpdateDto.Sku ?? "";
        b.Price = productUpdateDto.Price ?? 0;
        await db.SaveChangesAsync();
        return new CommandResultDto
        {
            Successfull = true,
            Message = "Products Updated!"
        };

    }).RequireAuthorization();
            app.MapDelete("products/delete/{guid}",
            async ([FromServices] LibraryDB db,
            [FromRoute] string guid) =>
            {
                var product = await db.Products.FirstOrDefaultAsync(m => m.Guid == guid);
                if (product == null)
                    return new CommandResultDto
                    {
                        Successfull = false,
                        Message = "Not Found Product!"
                    };
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Message = "Products Removed!"
                };
            }).RequireAuthorization();
        }
    }
}