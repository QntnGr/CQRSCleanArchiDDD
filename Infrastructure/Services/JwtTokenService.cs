

using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class JwtTokenService
{
    private readonly IOptions<JwtSettings> _configuration;

    public JwtTokenService(IOptions<JwtSettings> configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string userId)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.SecretKey));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            // Ajoutez d'autres claims si nécessaire
        };

        var token = new JwtSecurityToken(
            issuer: _configuration.Value.ValidIssuer,
            audience: _configuration.Value.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
