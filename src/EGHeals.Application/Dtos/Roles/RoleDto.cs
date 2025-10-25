namespace EGHeals.Application.Dtos.Roles
{
    public record RoleDto(Guid Id, string Name, IEnumerable<RolePermissionDto> RolePermissions);
}
