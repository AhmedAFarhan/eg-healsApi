using EGHeals.Application.Dtos.Roles;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Extensions.Roles
{
    public static class RoleExtensions
    {
        public static IEnumerable<RoleDto> ToRolesDtos(this IEnumerable<Role> roles)
        {
            return roles.Select(role => new RoleDto
            (
                Id: role.Id.Value,
                Name: role.Name,
                RolePermissions: role.Permissions.Select(permission => new RolePermissionDto
                (
                    Id: permission.Id.Value,
                    PermissionName: permission.Permission.Name
                ))
            ));
        }
    }
}
