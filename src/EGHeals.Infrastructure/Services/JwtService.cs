using EGHeals.Application.Services;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EGHeals.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings, IConfiguration configuration)
        {
            _jwtSettings = jwtSettings.Value;
            _configuration = configuration;
        }

        public string GenerateToken(SystemUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.Value.ToString()),
                new Claim("OwnershipId", user.OwnershipId.Value.ToString()),
                new Claim("FullName", user.FirstName + " " + user.LastName),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email ?? string.Empty),
                new Claim("Mobile", user.Mobile ?? string.Empty)
            };

            var roles = user.UserRoles.Select(r => r.Role.Name);
            var permissions = user.UserRoles.SelectMany(r => r.UserRolePermissions).Select(p => p.RolePermission.Permission.Name);

            claims.AddRange(roles.Select(r => new Claim("Roles", r)));
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
