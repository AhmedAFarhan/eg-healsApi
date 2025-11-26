namespace EGHeals.Application.Dtos.Shared.Roles.Requests
{
    public record RoleRequestDto(Guid Id, string Name, IEnumerable<PermissionRequestDto> Permissions);

}
