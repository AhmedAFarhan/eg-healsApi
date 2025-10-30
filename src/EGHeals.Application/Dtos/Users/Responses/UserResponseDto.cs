using EGHeals.Application.Dtos.Roles.Responses;

namespace EGHeals.Application.Dtos.Users.Responses
{
    public record UserResponseDto(Guid Id, string FirstName, string LastName, string Username, string? Email, string? PhoneNumber, Guid OwnershipId, IEnumerable<PermissionResponseDto> Permissions);
}
