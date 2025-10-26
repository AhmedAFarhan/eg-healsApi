using EGHeals.Application.Dtos.Users.Requests;


namespace EGHeals.Application.Features.Users.Commands.UpdateSubUser
{
    public record UpdateSubUserCommand(UpdateSubUserRequestDto User) : ICommand<UpdateSubUserResult>;
    public record UpdateSubUserResult(EGResponse<Guid> response);
}
