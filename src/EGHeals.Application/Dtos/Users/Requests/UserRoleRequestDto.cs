namespace EGHeals.Application.Dtos.Users.Requests
{
    public record UserRoleRequestDto(Guid RoleId, IEnumerable<UserRolePermissionRequestDto> RolePermissions);
}
