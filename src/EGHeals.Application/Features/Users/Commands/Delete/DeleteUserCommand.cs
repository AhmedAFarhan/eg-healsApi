namespace EGHeals.Application.Features.Users.Commands.Delete
{
    public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;
    public record DeleteUserResult(EGResponse<Guid> Response);
}
