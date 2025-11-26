using EGHeals.Application.Extensions.Shared.Users;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;


namespace EGHeals.Application.Features.Shared.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            //CHECK IF USER EXIST
            var user = await repo.GetByIdAsync(id: UserId.Of(query.Id),
                                               includeRoles: true,
                                               cancellationToken: cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var response = EGResponseFactory.Success(user.ToUserDto(), "Success operation.");

            return new GetUserByIdResult(response);
        }
    }
}
