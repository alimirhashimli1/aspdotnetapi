var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogs = new List <Blog>
{
    new Blog { Title = "My first blog", Body = "Blog 1"},
    new Blog { Title = "My second blog", Body = "Blog 2"},
    new Blog { Title = "My third blog", Body = "Blog 3"},
};

app.MapGet("/", () => "Root path!");
app.MapGet("blogs", ( ) => blogs);
app.MapGet("blogs/{id}", (int id ) => {
    if (id < 0 || id >= blogs.Count){
        return Results.NotFound();
    } else {
    return Results.Ok(blogs[id]);
    };
    });
app.MapPost("/blogs", (Blog blog) => {
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});
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
 
 public class Blog
 {
    public required string Title { get; set; }
    public required string Body { get; set; }
 }