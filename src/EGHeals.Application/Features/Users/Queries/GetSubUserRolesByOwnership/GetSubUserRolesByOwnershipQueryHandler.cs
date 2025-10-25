using EGHeals.Application.Contracts.Users;
using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Extensions.Users;

namespace EGHeals.Application.Features.Users.Queries.GetUserByIdByOwnership
{
    public class GetSubUserRolesByOwnershipQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubUserRolesByOwnershipQuery, EGResponse<UserDto>>
    {
        public async Task<EGResponse<UserDto>> Handle(GetSubUserRolesByOwnershipQuery query, CancellationToken cancellationToken)
        {
            //var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            ////CHECK IF USER EXIST
            //var user = await repo.GetSubUserRolesAsync(userId: query.SubUserId,cancellationToken: cancellationToken);
            //if (user is null)
            //{
            //    throw new NotFoundException("User not found");
            //}

            //var userDto = user.ToUserDto();

            //return new EGResponse<UserDto> { Success = true, Data = userDto };
            return null;
        }
    }
}
