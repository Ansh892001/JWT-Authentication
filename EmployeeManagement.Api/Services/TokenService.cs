using EmployeeManagement.Api.Configuration;
using EmployeeManagement.Api.Services.Interfaces;
using EmployeeManagement.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Api.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var securityKey = new SymmetricSecurityKey(
            Encoding.   UTF8.GetBytes(_jwtSettings.Key)
        );
        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler()
                .WriteToken(token);
    }
}