using JwtAspNet;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello");
app.MapGet("/login", GetToken);
app.MapGet("/restrito", () => "Segredo").RequireAuthorization();
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

string Page()
{
    return "Página secreta";
}