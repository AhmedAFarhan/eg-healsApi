using EGHeals.Application.Dtos.Shared.Users.Requests;

namespace EGHeals.Application.Features.Shared.Users.Commands.RegisterSubUser
{
    public record RegisterSubUserCommand(RegisterSubUserRequestDto User) : ICommand<RegisterUserResult>;
    public record RegisterUserResult(EGResponse<Guid> Response);
}
