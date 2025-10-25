using EGHeals.Application.Dtos.Users;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Extensions.Users
{
    public static class UserExtensions
    {
        public static UserDto ToUserDto(this SystemUser user)
        {
            return new UserDto
            (
                Id : user.Id.Value,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Username : user.UserName,
                Email : user.Email,
                Mobile : user.Mobile,
                OwnershipId: user.OwnershipId.Value,
                UserRoles : user.UserRoles.Select(role => new UserRoleDto
                (
                    Id: role.Id.Value,
                    RoleId: role.Role.Id.Value,
                    RoleName : role.Role.Name,
                    UserRolePermissions : role.UserRolePermissions.Select(permission => new UserRolePermissionDto
                    (
                        Id : permission.Id.Value,
                        //PermissionId: permission.RolePermission is not null ? permission.RolePermission.Permission.Id.Value : Guid.Empty,
                        RolePermissionId: permission.RolePermission.Id.Value,
                        PermissionName : permission.RolePermission is not null ? permission.RolePermission.Permission.Name : "Permission"
                    ))
                ))
            );
        }
        public static IEnumerable<SubUserDto> ToSubUsersDtos(this IEnumerable<SystemUser> users)
        {
            return users.Select(user => new SubUserDto
            (
                Id: user.Id.Value,
                Username: user.UserName,
                UserRoles: user.UserRoles.Select(role => new SubUserRoleDto
                (
                    Id: role.Id.Value,
                    RoleName: role.Role.Name
                ))
            ));
        }
    }
}
