namespace EGHeals.Application.Features.Shared.Users.Commands.Activate
{
    public record ActivateUserCommand(Guid Id) : ICommand<ActivateUserResult>;
    public record ActivateUserResult(EGResponse<Guid> Response);
}
