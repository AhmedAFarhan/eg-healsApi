namespace EGHeals.Application.Dtos.Roles.Responses
{
    public record RoleResponseDto(Guid Id, string Name, IEnumerable<PermissionResponseDto> Permissions);
}
