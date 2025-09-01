using System.Security.Claims;
using System.Text;
using JwtAspNet;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", p => p.RequireRole("admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello");
app.MapGet("/login", GetToken);
app.MapGet("/restrito", () => "Acesso restrito").RequireAuthorization();
app.MapGet("/admin", () => "Admin only").RequireAuthorization("Admin");
app.Run();

string GetToken (TokenService tokenService)
{
    var user = new User(
        42,
        "Alphafa",
        "abc@alphabet.com",
        "",
        "123456",
        new[] {"student", "premium", "admin" });
    
    return tokenService.CreateToken(user);
}