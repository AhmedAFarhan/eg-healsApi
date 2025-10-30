using EGHeals.Application.Dtos.Roles.Responses;
using EGHeals.Application.Extensions.Roles;
using EGHeals.Application.Services.Roles;
using EGHeals.Infrastructure.Data;

namespace EGHeals.Infrastructure.Services.Roles
{
    public class RoleQueryService(ApplicationIdentityDbContext dbContext) : IRoleQueryService
    {
        public async Task<IEnumerable<RoleResponseDto>> GetRolesWithPermissionsByActivityType(UserActivity? userActivity,
                                                                                              bool isActive,
                                                                                              CancellationToken cancellationToken = default)
        {
            var roles = await dbContext.Roles.AsNoTracking()
                                             .Include(x => x.Permissions.Where(p => p.IsActive == isActive))
                                                     .ThenInclude(x => x.Permission)
                                             .Where(x => x.UserActivity == userActivity && x.IsActive == isActive && !x.IsAdmin && !x.IsDeleted)
                                             .ToListAsync(cancellationToken);
            return roles.ToRolesDtos();
        }
    }
}
