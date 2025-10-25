using EGHeals.Application.Dtos.Users;

namespace EGHeals.Application.Features.Users.Queries.GetUserByIdByOwnership
{
    public record GetSubUserRolesByOwnershipQuery(Guid SubUserId) : IQuery<EGResponse<UserDto>>;
}
