using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUserPermissionsByOwnership
{
    public class GetSubUserPermissionsByOwnershipQueryHandler(IUnitOfWork unitOfWork,
                                                              IUserQueryService userQueryService) : IQueryHandler<GetSubUserPermissionsByOwnershipQuery, GetSubUserPermissionsByOwnershipResult>
    {
        public async Task<GetSubUserPermissionsByOwnershipResult> Handle(GetSubUserPermissionsByOwnershipQuery query, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            //CHECK IF USER EXIST
            var user = await repo.GetByIdAsync(id: UserId.Of(query.SubUserId),
                                               includeOwnershipId: true,
                                               cancellationToken: cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var userPermissions = await userQueryService.GetUserWithPermissions(id: UserId.Of(query.SubUserId), cancellationToken: cancellationToken);

            var response = EGResponseFactory.Success<UserResponseDto>(userPermissions, "Success operation.");

            return new GetSubUserPermissionsByOwnershipResult(response);
        }
    }
}
