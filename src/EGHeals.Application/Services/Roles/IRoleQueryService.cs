using EGHeals.Application.Dtos.Roles.Responses;

namespace EGHeals.Application.Services.Roles
{
    public interface IRoleQueryService
    {
        Task<IEnumerable<RoleResponseDto>> GetRolesWithPermissionsByActivityType(UserActivity? userActivity,
                                                                                 bool isActive,
                                                                                 CancellationToken cancellationToken = default);
    }
}
