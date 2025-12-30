namespace EGHeals.Application.Dtos.Shared.Users.Responses
{
    public record UserResponseDto(Guid Id, string FirstName, string LastName, string Username, string? Email, string? PhoneNumber, bool IsActive, IEnumerable<UserRoleResponseDto> Roles);
}
