using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Application.Services.Jwt;
using EGHeals.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EGHeals.Infrastructure.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(UserResponseDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("OwnershipId", user.OwnershipId.ToString()),
                new Claim("FullName", user.FirstName + " " + user.LastName),
                new Claim("UserName", user.Username),
                new Claim("Email", user.Email ?? string.Empty),
                new Claim("PhoneNumber", user.PhoneNumber ?? string.Empty)
            };

            var permissions = user.Permissions.Select(r => r.Name);

            claims.AddRange(permissions.Select(p => new Claim("Permissions", p)));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
