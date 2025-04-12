using JwtBearer.Models;
using JwtBearer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();


var app = builder.Build();

app.MapGet("/", (TokenService service) =>
    service.Generate(new User(
        1, 
        "teste@gmail.com", 
        "123", new[] 
        {
            "premium", "tech"
        })));

app.Run();
