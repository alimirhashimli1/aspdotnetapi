var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Root path!");
app.MapGet("/downloads", () => "Downloads!");
app.MapPut("/", () => "This is a put!");
app.MapDelete("/", () => "This is a delete!");
app.MapPost("/", () => "This is a POst!");
app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) => {
    return $"User id: {userId}, Post ID: {slug}";
});
app.MapGet("/products/{id:int:min(0)}", (int id) => {
    return $"Product id: {id}";
});

app.MapGet("/products/{category}/{id}/{year?}", (string category,  int id, int? year) => {
        var cleanCategory = category.Replace("-", " ");
    return $" This {cleanCategory} is produced in {year}. Product id: {id}";
});

app.MapGet("/files/{*filePath}", (string filePath) => {
    return $"File path: {filePath}";
});

app.MapGet("/search", (string? q, int page =1) => {
    return $"Searching for {q} on page {page}";
});

app.MapGet("/store/{category}/{productId:int?}/{*extraPath}", (string category, int? productId, string? extraPath, bool inStock = true) => {
    return $"{category} {productId} {extraPath} {inStock}";
});
app.Run();
 