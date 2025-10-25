using EGHeals.Application.Dtos.Users;

namespace EGHeals.Application.Features.Users.Commands.Register
{
    public record RegisterUserCommand(RegisterAdminUserDto User) : ICommand<RegisterUserResult>;
    public record RegisterUserResult(EGResponse<Guid> response);
}
