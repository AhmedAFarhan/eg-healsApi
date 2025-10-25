using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Extensions.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByOwnership
{
    public class GetSubUsersByOwnershipQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubUsersByOwnershipQuery, GetSubUsersByOwnershipResult>
    {
        public async Task<GetSubUsersByOwnershipResult> Handle(GetSubUsersByOwnershipQuery query, CancellationToken cancellationToken)
        {
            var userRepo = unitOfWork.GetCustomRepository<IUserRepository>();

            var users = await userRepo.GetSubUsersAsync(options: query.QueryOptions, cancellationToken:cancellationToken);

            var totalCount = await userRepo.GetSubUsersCountAsync(filters: query.QueryOptions.QueryFilters, cancellationToken: cancellationToken);

            var pagination = new PaginatedResult<SubUserDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, totalCount, users.ToSubUsersDtos());

            var response = EGResponseFactory.Success<PaginatedResult<SubUserDto>>(pagination, "Success operation.");

            return new GetSubUsersByOwnershipResult(response);
        }
    }
}
