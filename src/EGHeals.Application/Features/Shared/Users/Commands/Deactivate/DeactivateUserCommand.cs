namespace EGHeals.Application.Features.Shared.Users.Commands.Deactivate
{
    public record DeactivateUserCommand(Guid Id) : ICommand<DeactivateUserResult>;
    public record DeactivateUserResult(EGResponse<Guid> Response);
}
