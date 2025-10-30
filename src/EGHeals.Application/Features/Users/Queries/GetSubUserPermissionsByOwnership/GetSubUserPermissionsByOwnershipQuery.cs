using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Dtos.Users.Responses;

namespace EGHeals.Application.Features.Users.Queries.GetSubUserPermissionsByOwnership
{
    public record GetSubUserPermissionsByOwnershipQuery(Guid SubUserId) : IQuery<GetSubUserPermissionsByOwnershipResult>;
    public record GetSubUserPermissionsByOwnershipResult(EGResponse<UserResponseDto> response);
}
