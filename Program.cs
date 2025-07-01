using backend.practice.DbContextes;
using backend.practice.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    var builder = WebApplication.CreateBuilder(args);
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddOpenApi();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}

app.UseHttpsRedirection();
app.MapGet("members/list", () =>
{
    using var db = new LibraryDB();
    return db.Members.ToList();

});
app.MapPost("members/create", (Member member) =>
{
    using var db = new LibraryDB();
    db.Members.Add(member);
    db.SaveChanges();
    return "Member Created!";
});
app.MapPut("members/update{id}", (int id, Member member) =>
{
    using var db = new LibraryDB();
    var m = db.Members.Find(id);
    if (m == null)
    {
        return "Member Not Found";
    }
    m.Firstname = member.Firstname;
    m.Lastname = member.Lastname;
    m.Gender = member.Gender;
    db.SaveChanges();
    return "Member updated!";
});
app.MapDelete("Members/remove{id}", (int id) =>
{
    using var db = new LibraryDB();
    var member = db.Members.Find(id);
    if (member == null)
    {
        return "Member Not Found";
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return "Member Removed!";
});


app.MapGet("books/list", () =>
{
    using var db = new LibraryDB();
    return db.Books.ToList();
});
app.MapPost("books/create", (Book book) =>
{
    using var db = new LibraryDB();
    db.Books.Add(book);
    db.SaveChanges();
    return "Book Created!";
});
app.MapPut("books/update{id}", (int id, Book book) =>
{
    using var db = new LibraryDB();
    var b = db.Books.Find(id);
    if (b == null)
    {
        return "Book Not Found";
    }
    b.title = book.title;
    b.writer = book.writer;
    b.publisher = book.publisher;
    b.price = book.price;
    db.SaveChanges();
    return "Book updated!";
});
app.MapDelete("books/remove{id}", (int id) =>
{
    using var db = new LibraryDB();
    var book = db.Books.Find(id);
    if (book == null)
    {
        return "Book Not Found";
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return "Book Removed!";
});

app.Run();
