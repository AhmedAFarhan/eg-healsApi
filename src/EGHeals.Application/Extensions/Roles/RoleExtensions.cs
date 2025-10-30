using EGHeals.Application.Dtos.Roles.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Extensions.Roles
{
    public static class RoleExtensions
    {
        public static IEnumerable<RoleResponseDto> ToRolesDtos(this IEnumerable<Role> roles)
        {
            return roles.Select(role => new RoleResponseDto
            (
                Id: role.Id.Value,
                Name: role.Name,
                Permissions: role.Permissions.Select(permission => new PermissionResponseDto
                (
                    Id: permission.Id.Value,
                    Name: permission.Permission.Name
                ))
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
