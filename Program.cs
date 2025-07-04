var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
// app.MapGet("products/List", () =>
// {
//     return "Products List";
// });
// app.MapPost("products/Create", () =>
// {
//     return "Products Created!";
// });
// app.MapPut("products/Update", () =>
// {
//     return "Products Updated!";
// });
// app.MapDelete("products/Delete", () =>
// {
//     return "Products Removed!";
// });

app.Run();

