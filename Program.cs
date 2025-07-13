using Microsoft.AspNetCore.Mvc;
using ToytoyStoreBackend.DbContextes;
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
app.MapGet("members/List", ([FromServices] LibraryDB db) =>
{
    return db.Members.ToList();

});
app.MapPost("members/Create", ([FromServices] LibraryDB db, [FromBody] Member member) =>
{
    db.Members.Add(member);
    db.SaveChanges();
    return "user Created!";
});
app.MapPut("members/Update/{id}", ([FromServices] LibraryDB db, [FromRoute] int id, [FromBody] Member member) =>
{
    var b = db.Members.Find(id);
    if (b == null)
    {
        return new { Message = "Not Found usert!" };
    }
    b.Name = member.Name;
    b.Family = member.Family;
    b.UserName = member.UserName;
    b.Password = member.Password;
    db.SaveChanges();
    return new { Message = "user Updated!" };
});
app.MapDelete("members/Delete/{id}", ([FromServices] LibraryDB db, [FromRoute] int id) =>
{
    var member = db.Members.Find(id);
    if (member == null)
    {
        return new { IsOk = false, Result = "Not Found user" };
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return new { IsOk = true, Result = "User Removed!" };
});
app.MapGet("products/List", ([FromServices] LibraryDB db) =>
{
    // using var db = new LibraryDB();
    return db.Products.ToList();
    //  "Products List";
});
app.MapPost("products/Create", ([FromServices] LibraryDB db, [FromBody] Product product) =>
{
    // using var db = new LibraryDB();
    db.Products.Add(product);
    db.SaveChanges();

    return "Products Created!";
});
app.MapPut("products/Update/{id}", ([FromServices] LibraryDB db, [FromRoute] int id, [FromBody] Product product) =>
{
    // using var db = new LibraryDB();
    var b = db.Products.Find(id);
    if (b == null)
    {
        return new { Message = "Not Found Product!" };
    }
    b.Productname = product.Productname;
    b.Description = product.Description;
    b.Brand = product.Brand;
    b.Category = product.Category;
    b.Sku = product.Sku;
    db.SaveChanges();
    return new { Message = "Products Updated!" };
});
app.MapDelete("products/Delete/{id}", ([FromServices] LibraryDB db, [FromRoute] int id) =>
{
    // using var db = new LibraryDB();
    var product = db.Products.Find(id);
    if (product == null)
    {
        return new { IsOk = false, Result = "Not Found Product" };
    }
    db.Products.Remove(product);
    db.SaveChanges();
    return new { IsOk = true, Result = "Products Removed!" };
});

// ff
app.Run();

