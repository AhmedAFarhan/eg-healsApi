using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Application.Dtos.Users;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByOwnership
{
    public record GetSubUsersByOwnershipQuery(QueryOptions<SystemUser> QueryOptions) : IQuery<GetSubUsersByOwnershipResult>;
    public record GetSubUsersByOwnershipResult(EGResponse<PaginatedResult<SubUserDto>> response);

}
