namespace Services.Authen.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using global::Services.Authen.Domain.Entities;
using global::Services.Authen.Infrastructure.Repositories;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _key;

    public JwtTokenGenerator(string key)
    {
        _key = key;
    }

    public string GenerateToken(User user)
    {
        var claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "auth_server", audience: "auth_client", claims: claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
