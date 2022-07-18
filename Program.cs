using DsiMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDsiMiddleware();

app.MapGet("/test", (HttpRequest request) => {
    return Results.Ok("Test successful!");
});

app.Run();