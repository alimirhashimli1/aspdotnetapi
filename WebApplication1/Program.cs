var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Root path!");
app.MapGet("/downloads", () => "Downloads!");
app.MapPut("/", () => "This is a put!");
app.MapDelete("/", () => "This is a delete!");
app.MapPost("/", () => "This is a POst!");

app.Run();
 