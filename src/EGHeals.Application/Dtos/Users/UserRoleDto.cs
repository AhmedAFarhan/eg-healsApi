namespace EGHeals.Application.Dtos.Users
{
    public record UserRoleDto(Guid Id, Guid RoleId, string RoleName, IEnumerable<UserRolePermissionDto> UserRolePermissions);
}
