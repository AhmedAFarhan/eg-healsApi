using EGHeals.Application.Dtos.Shared.Users.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Features.Shared.Users.Queries.GetSubUsersByOwnership
{
    public record GetSubUsersByTenantQuery(QueryOptions<User> QueryOptions) : IQuery<GetSubUsersByTenantResult>;
    public record GetSubUsersByTenantResult(EGResponse<PaginatedResult<UserResponseDto>> Response);

}
