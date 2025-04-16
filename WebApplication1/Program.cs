var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging((o) => {});


var app = builder.Build();

app.UseHttpLogging();
app.Use(async (context, next) => {
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

var blogs = new List <Blog>
{
    new Blog { Title = "My first blog", Body = "Blog 1"},
    new Blog { Title = "My second blog", Body = "Blog 2"},
    new Blog { Title = "My third blog", Body = "Blog 3"},
};

app.MapGet("/", () => "Root path!");
app.MapGet("/hello", () => "This is a hello !");
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

app.MapDelete("/blogs/{id}", (int id) => {
    if (id < 0 || id >= blogs.Count){
        return Results.NotFound();
    } else {
        blogs.RemoveAt(id);
    return Results.NoContent();
    };
});

app.MapPut("/blogs/{id}", (int id, Blog blog) => {
    if (id < 0 || id >= blogs.Count){
        return Results.NotFound();
    } else {
    blogs[id] = blog;
    return Results.Ok(blog);
    };
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