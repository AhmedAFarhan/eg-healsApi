namespace EGHeals.Application.Dtos.Shared.Users.Requests
{
    public record RegisterSubUserRequestDto(string FirstName, string LastName, string Email, string Password, IEnumerable<UserRoleRequestDto> UserRoles);
}
