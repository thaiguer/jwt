using JwtAspNet.Models;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", GetToken);

app.Run();

string GetToken (TokenService tokenService)
{
    var user = new User(
        42,
        "Alphafa",
        "abc@alphabet.com",
        "",
        "123456",
        new[] {"student", "premium" });
    
    return tokenService.CreateToken(user);
}