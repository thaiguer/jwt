using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAspNet.Models;

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

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(securityTokenDescriptor);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Name));

        return claimsIdentity;
    }
}