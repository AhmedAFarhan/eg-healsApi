using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Services
{
    public interface IJwtService
    {
        public string GenerateToken(SystemUser user);
    }
}
