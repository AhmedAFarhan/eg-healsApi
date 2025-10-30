using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByOwnership
{
    public record GetSubUsersByOwnershipQuery(QueryOptions<User> QueryOptions) : IQuery<GetSubUsersByOwnershipResult>;
    public record GetSubUsersByOwnershipResult(EGResponse<PaginatedResult<SubUserResponseDto>> response);

}
