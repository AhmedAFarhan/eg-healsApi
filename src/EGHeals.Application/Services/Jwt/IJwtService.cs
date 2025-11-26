
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Services.Jwt
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
