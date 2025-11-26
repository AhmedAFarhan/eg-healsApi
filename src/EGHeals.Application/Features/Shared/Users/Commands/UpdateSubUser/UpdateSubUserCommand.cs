using EGHeals.Application.Dtos.Shared.Users.Requests;

namespace EGHeals.Application.Features.Shared.Users.Commands.UpdateSubUser
{
    public record UpdateSubUserCommand(Guid Id, UpdateSubUserRequestDto User) : ICommand<UpdateSubUserResult>;
    public record UpdateSubUserResult(EGResponse<Guid> Response);
}
