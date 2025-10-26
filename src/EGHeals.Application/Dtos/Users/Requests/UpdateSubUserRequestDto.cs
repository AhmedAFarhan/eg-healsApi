namespace EGHeals.Application.Dtos.Users.Requests
{
    public record UpdateSubUserRequestDto(Guid Id, string FirstName, string LastName, string Email, string Password);
}
