using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Contracts.Roles;
using EGHeals.Application.Dtos.Roles;
using EGHeals.Application.Extensions.Roles;

namespace EGHeals.Application.Features.Users.Queries.GetRoles
{
    public class GetRolesQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IQueryHandler<GetRolesQuery, GetRolesResult>
    {
        public async Task<GetRolesResult> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetCustomRepository<IRoleRepository>();

            //Get Owner information
            //TODO : get owner by Id based on userId for ICurrentUserService
            //TODO : fetch the activity type

            var roles = await repo.GetRolesAsync(UserActivity.RADIOLOGY, cancellationToken: cancellationToken);

            var rolesDtos = roles.ToRolesDtos();

            var response = EGResponseFactory.Success<IEnumerable<RoleDto>>(rolesDtos, "Success operation.");

            return new GetRolesResult(response); 
        }
    }
}
