namespace EGHeals.Application.Features.Users.Commands.Activate
{
    public record ActivateUserCommand(Guid Id) : ICommand<ActivateUserResult>;
    public record ActivateUserResult(EGResponse<Guid> Response);
}
