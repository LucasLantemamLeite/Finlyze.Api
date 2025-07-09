using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Finlyze.Api.Services.Jwt;
using Finlyze.Domain.Entity;
using Microsoft.IdentityModel.Tokens;

namespace Finlyze.Api.Services.TokenHandler;

public class JwtTokenHandler
{
    public static string GenerateToken(UserAccount user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtSettings.JwtKey);
        var credencials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.Name.Value),
                new Claim("Active", user.Active.Value.ToString()),
               new Claim(ClaimTypes.Role, ((int)user.Role.Value).ToString())
            ]),

            SigningCredentials = credencials,
            Expires = DateTime.UtcNow.AddHours(6)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}