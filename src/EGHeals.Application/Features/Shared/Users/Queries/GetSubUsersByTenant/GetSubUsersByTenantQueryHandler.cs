using EGHeals.Application.Dtos.Shared.Users.Responses;
using EGHeals.Application.Extensions.Shared.Users;
using EGHeals.Application.Features.Shared.Users.Queries.GetSubUsersByOwnership;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;

namespace EGHeals.Application.Features.Shared.Users.Queries.GetSubUsersTenant
{
    public class GetSubUsersByTenantQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubUsersByTenantQuery, GetSubUsersByTenantResult>
    {
        public async Task<GetSubUsersByTenantResult> Handle(GetSubUsersByTenantQuery query, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            var users = await repo.GetAllAsync(options: query.QueryOptions,
                                               includeRoles: true,
                                               cancellationToken: cancellationToken);

            var totalCount = await repo.GetCountAsync(filters: query.QueryOptions.QueryFilters,
                                                      cancellationToken: cancellationToken);

            var pagination = new PaginatedResult<UserResponseDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, totalCount, users.ToSubUsersDtos());

            var response = EGResponseFactory.Success(pagination, "Success operation.");

            return new GetSubUsersByTenantResult(response);
        }
    }
}
