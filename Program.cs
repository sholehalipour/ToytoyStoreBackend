using Microsoft.AspNetCore.Mvc;
using ToytoyStoreBackend.DbContextes;
using ToytoyStoreBackend.DTOs.Common;
using ToytoyStoreBackend.DTOs.Members;
using ToytoyStoreBackend.DTOs.Products;
using ToytoyStoreBackend.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<LibraryDB>();
var app = builder.Build();


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
app.MapGet("members/List",
 ([FromServices] LibraryDB db) =>
{
    return db.Members.Select(b => new MemberListDto
    {
        Id = b.Guid,
        Name = b.Name,
        Family = b.Family,
        UserName = b.UserName,
    }).ToList();

});
app.MapPost("members/Create", (
    [FromServices] LibraryDB db,
     [FromBody] MemberAddDto memberAddDto) =>
{
    var member = new Member
    {
        Name = !string.IsNullOrEmpty(memberAddDto.Name) ? memberAddDto.Name : "بدون نام",
        Family = !string.IsNullOrEmpty(memberAddDto.Family) ? memberAddDto.Family : " بدون نام خانوادگی",
        UserName = !string.IsNullOrEmpty(memberAddDto.UserName) ? memberAddDto.UserName : "بدون نام کاربری",
        Password = !string.IsNullOrEmpty(memberAddDto.Password) ? memberAddDto.Password : "بدون رمز عبور",

    };
    db.Members.Add(member);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Message = "user Created!"
    };

});
app.MapPut("members/Update/{guid}",
 ([FromServices] LibraryDB db,
  [FromRoute] string guid,
   [FromBody] MemberUpdateDto memberUpdateDto) =>
{
    var b = db.Members.FirstOrDefault(m => m.Guid == guid);
    if (b == null)
    {
        return new CommandResultDto
        {
            Successfull = false,

            Message = "Not Found usert!"
        };
    }
    b.Name = memberUpdateDto.Name ?? "بدون نام";
    b.Family = memberUpdateDto.Family;
    b.UserName = memberUpdateDto.UserName;
    // b.Password = memberUpdateDto.Password;
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Message = "user Updated!"
    };
});
app.MapDelete("members/Delete/{guid}",
 ([FromServices] LibraryDB db,
  [FromRoute] string guid) =>
{
    var member = db.Members.FirstOrDefault(m => m.Guid == guid);
    if (member == null)
    {
        return new CommandResultDto
        {
            Successfull = false,
            Message = "Not Found user"
        };
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Message = "User Removed!"
    };
});
app.MapGet("products/List",
 ([FromServices] LibraryDB db) =>
{

    return db.Products.Select(b => new ProductListDto
    {
        Id = b.Guid,
        ProductName = b.ProductName,
        Description = b.Description,
        Brand = b.Brand,
        Category = b.Category,
        Sku = b.Sku,
        Price = b.Price,
    }).ToList();
});
app.MapPost("products/Create",
 ([FromServices] LibraryDB db,
 [FromBody] ProductAddDto ProductAddDto) =>
{
    var product = new Product
    {
        ProductName = !string.IsNullOrEmpty(ProductAddDto.ProductName) ? ProductAddDto.ProductName : " بدون نام محصول",
        Description = !string.IsNullOrEmpty(ProductAddDto.Description) ? ProductAddDto.Description : " بدون توضیح",
        Category = !string.IsNullOrEmpty(ProductAddDto.Category) ? ProductAddDto.Category : "بدون دسته بندی",
        Brand = !string.IsNullOrEmpty(ProductAddDto.Brand) ? ProductAddDto.Brand : "بدون برند",
        Sku = !string.IsNullOrEmpty(ProductAddDto.Sku) ? ProductAddDto.Sku : "بدون شماره سریال",
        Price = ProductAddDto.Price ?? 0,
    };
    db.Products.Add(product);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Message = "Products Created!"
    };
});
app.MapPut("products/Update/{guid}",
 ([FromServices] LibraryDB db,
 [FromRoute] string guid,
 [FromBody] ProductUpdateDto productUpdateDto) =>
{

    var b = db.Products.FirstOrDefault(m => m.Guid == guid);
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
    b.Category = productUpdateDto.Category ?? "";
    b.Sku = productUpdateDto.Sku ?? "";
    b.Price = productUpdateDto.Price ?? 0;
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,
        Message = "Products Updated!"
    };

});
app.MapDelete("products/Delete/{guid}",
([FromServices] LibraryDB db,
[FromRoute] string guid) =>
{

    var product = db.Products.FirstOrDefault(m => m.Guid == guid);
    if (product == null)
        return new CommandResultDto
        {
            Successfull = false,

            Message = "Not Found Product!"
        };
    db.Products.Remove(product);
    db.SaveChanges();
    return new CommandResultDto
    {
        Successfull = true,

        Message = "Products Removed!"
    };


});
app.Run();

