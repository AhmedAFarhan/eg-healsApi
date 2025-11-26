using EGHeals.Application.Dtos.Shared.Roles.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Features.Shared.Users.Queries.GetRolesByTenant
{ 
    public record GetRolesByTenantQuery(QueryOptions<Role> QueryOptions) : IQuery<GetRolesByTenantResult>;
    public record GetRolesByTenantResult(EGResponse<PaginatedResult<RoleResponseDto>> Response);
}
