namespace EGHeals.Application.Dtos.Shared.Users.Requests
{
    public record LoginUserRequestDto(string Username, string Password, string ClientId, string ClientSecret);
}
