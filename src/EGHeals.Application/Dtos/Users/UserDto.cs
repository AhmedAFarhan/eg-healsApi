namespace EGHeals.Application.Dtos.Users
{
    public record UserDto(Guid Id, string FirstName, string LastName, string Username, string? Email, string? Mobile, Guid OwnershipId, IEnumerable<UserRoleDto> UserRoles);
}
