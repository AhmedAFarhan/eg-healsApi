using EGHeals.Application.Dtos.Users.Requests;

namespace EGHeals.Application.Features.Users.Commands.RegisterSubUser
{
    public record RegisterSubUserCommand(RegisterSubUserRequestDto User) : ICommand<RegisterUserResult>;
    public record RegisterUserResult(EGResponse<Guid> Response);
}
