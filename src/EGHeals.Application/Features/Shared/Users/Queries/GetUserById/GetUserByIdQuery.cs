using EGHeals.Application.Dtos.Shared.Users.Responses;

namespace EGHeals.Application.Features.Shared.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;
    public record GetUserByIdResult(EGResponse<UserResponseDto> Response);
}
