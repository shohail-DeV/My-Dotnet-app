var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World! This is a demonstration of .NET 8 Deployment");

app.Run();
