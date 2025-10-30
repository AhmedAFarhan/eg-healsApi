using EGHeals.Application.Dtos.Users.Requests;


namespace EGHeals.Application.Features.Users.Commands.UpdateSubUser
{
    public record UpdateSubUserCommand(Guid Id, UpdateSubUserRequestDto User) : ICommand<UpdateSubUserResult>;
    public record UpdateSubUserResult(EGResponse<Guid> Response);
}
