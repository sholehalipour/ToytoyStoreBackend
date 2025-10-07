using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.DTOs.Common;
using ToytoyStoreBackend.DTOs.Products;
using ToytoyStoreBackend.Entities;

namespace ToytoyStoreBackend.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndPoints(this WebApplication app)
        {
            app.MapGet("products/List", async ([FromServices] LibraryDB db) =>
{

    var result = await db.Products.Select(b => new ProductListDto
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
            app.MapPost("products/Create",
             async ([FromServices] LibraryDB db,
             [FromBody] ProductAddDto ProductAddDto) =>
            {
                var product = new Product
                {
                    ProductName = !string.IsNullOrEmpty(ProductAddDto.ProductName) ? ProductAddDto.ProductName : " بدون نام محصول",
                    Description = !string.IsNullOrEmpty(ProductAddDto.Description) ? ProductAddDto.Description : " بدون توضیح",
                    // Category = !string.IsNullOrEmpty(ProductAddDto.Category.Name) ? ProductAddDto.Category.Name : "بدون دسته بندی",
                    Brand = !string.IsNullOrEmpty(ProductAddDto.Brand) ? ProductAddDto.Brand : "بدون برند",
                    Sku = !string.IsNullOrEmpty(ProductAddDto.Sku) ? ProductAddDto.Sku : "بدون شماره سریال",
                    Price = ProductAddDto.Price ?? 0,
                };

                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return new CommandResultDto
                {
                    Successfull = true,
                    Message = "Products Created!"
                };
            });
            app.MapPut("products/Update/{guid}",
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

            });
            app.MapDelete("products/Delete/{guid}",
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


            });
        }
    }
}