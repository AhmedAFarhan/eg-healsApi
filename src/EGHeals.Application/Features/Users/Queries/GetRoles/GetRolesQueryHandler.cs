using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Dtos.Roles.Responses;
using EGHeals.Application.Services.Roles;

namespace EGHeals.Application.Features.Users.Queries.GetRoles
{
    public class GetRolesQueryHandler(IUnitOfWork unitOfWork,
                                      IRoleQueryService roleQueryService,
                                      ICurrentUserService currentUserService) : IQueryHandler<GetRolesQuery, GetRolesResult>
    {
        public async Task<GetRolesResult> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            //Get Owner information
            //TODO : get owner by Id based on userId for ICurrentUserService
            //TODO : fetch the activity type

            var roles = await roleQueryService.GetRolesWithPermissionsByActivityType(userActivity: UserActivity.RADIOLOGY,
                                                                                     isActive: true,
                                                                                     cancellationToken: cancellationToken);


            var response = EGResponseFactory.Success<IEnumerable<RoleResponseDto>>(roles, "Success operation.");

            return new GetRolesResult(response); 
        }
    }
}
