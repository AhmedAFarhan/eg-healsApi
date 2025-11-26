namespace EGHeals.Application.Features.Shared.Users.Commands.Delete
{
    public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;
    public record DeleteUserResult(EGResponse<Guid> Response);
}
