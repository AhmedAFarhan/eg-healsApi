using EGHeals.Application.Dtos.Shared.Roles.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Extensions.Shared.Roles
{
    public static class RoleExtensions
    {
        public static IEnumerable<RoleResponseDto> ToRolesDtos(this IEnumerable<Role> roles)
        {
            return roles.Select(role => new RoleResponseDto
            (
                Id: role.Id.Value,
                Name: role.Name,
                IsActive: role.IsActive
            ));
        }
        public static IEnumerable<PermissionResponseDto> ToPermissionsDtos(this IEnumerable<Permission> roles)
        {
            return roles.Select(role => new PermissionResponseDto
            (
                Id: role.Id.Value,
                Name: role.Name
            ));
        }
    }
}
