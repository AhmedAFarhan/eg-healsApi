using EGHeals.Application.Dtos.Shared.Roles.Responses;
using EGHeals.Application.Extensions.Shared.Roles;
using EGHeals.Application.Features.Shared.Users.Queries.GetRolesByTenant;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Application.Features.Shared.Users.Queries.GetRoles
{
    public class GetRolesByTenantQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetRolesByTenantQuery, GetRolesByTenantResult>
    {
        public async Task<GetRolesByTenantResult> Handle(GetRolesByTenantQuery query, CancellationToken cancellationToken)
        {
            var roleRepo = unitOfWork.GetRepository<Role, RoleId>();

            // Get Roles
            var roles = await roleRepo.GetAllAsync(options: query.QueryOptions,
                                                   cancellationToken: cancellationToken);

            var totalCount = await roleRepo.GetCountAsync(filters: query.QueryOptions.QueryFilters,
                                                          cancellationToken: cancellationToken);

            var pagination = new PaginatedResult<RoleResponseDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, totalCount, roles.ToRolesDtos());

            var response = EGResponseFactory.Success(pagination, "Success operation.");

            return new GetRolesByTenantResult(response);

        }
    }
}
