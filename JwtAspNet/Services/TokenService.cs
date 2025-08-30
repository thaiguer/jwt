using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAspNet.Services;

public class TokenService
{
    public string CreateToken()
    {
        var byteKey = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
        var symmetricSecurityKey = new SymmetricSecurityKey(byteKey);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Expires = DateTime.UtcNow.AddHours(12)
        };

        new Claim(ClaimTypes.Name, "");
        new Claim(ClaimTypes.Role, "");

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(securityTokenDescriptor);
        return handler.WriteToken(token);
    }
}