using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Application.Contracts.Roles
{
    public interface IRoleRepository : IBaseRepository<Role, RoleId>
    {
        Task<IEnumerable<Role>> GetRolesAsync(UserActivity? type, CancellationToken cancellationToken = default);
    }
}
