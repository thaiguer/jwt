using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", GetToken);

app.Run();

string GetToken (TokenService tokenService)
{
    return tokenService.CreateToken();
}