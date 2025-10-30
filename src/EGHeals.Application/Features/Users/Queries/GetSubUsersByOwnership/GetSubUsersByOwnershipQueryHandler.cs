using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByOwnership
{
    public class GetSubUsersByOwnershipQueryHandler(IUnitOfWork unitOfWork,
                                                    IUserQueryService userQueryService) : IQueryHandler<GetSubUsersByOwnershipQuery, GetSubUsersByOwnershipResult>
    {
        public async Task<GetSubUsersByOwnershipResult> Handle(GetSubUsersByOwnershipQuery query, CancellationToken cancellationToken)
        {
            var userRepo = unitOfWork.GetCustomRepository<IUserRepository>();

            var users = await userQueryService.GetSubUsersAsync(options: query.QueryOptions,
                                                                includeOwnership:true,
                                                                cancellationToken: cancellationToken);

            var totalCount = await userRepo.GetSubUsersCountAsync(filters: query.QueryOptions.QueryFilters,
                                                                  includeOwnership: true,
                                                                  cancellationToken: cancellationToken);

            var pagination = new PaginatedResult<SubUserResponseDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, totalCount, users);

            var response = EGResponseFactory.Success<PaginatedResult<SubUserResponseDto>>(pagination, "Success operation.");

            return new GetSubUsersByOwnershipResult(response);
        }
    }
}
