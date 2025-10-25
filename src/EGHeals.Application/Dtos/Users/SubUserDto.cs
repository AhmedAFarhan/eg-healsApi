namespace EGHeals.Application.Dtos.Users
{
    public record SubUserDto(Guid Id, string Username, IEnumerable<SubUserRoleDto> UserRoles);
}
