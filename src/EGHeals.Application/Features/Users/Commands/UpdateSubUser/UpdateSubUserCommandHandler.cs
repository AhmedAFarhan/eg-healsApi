using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services;

namespace EGHeals.Application.Features.Users.Commands.UpdateSubUser
{
    public class UpdateSubUserCommandHandler(IUnitOfWork unitOfWork,
                                             IIdentityService identityService) : ICommandHandler<UpdateSubUserCommand, UpdateSubUserResult>
    {
        public async Task<UpdateSubUserResult> Handle(UpdateSubUserCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist
            var userExist = await repo.GetByIdAsync(id: SystemUserId.Of(command.User.Id),
                                                    includeOwnership: true,
                                                    includeDeleted: false,
                                                    cancellationToken:cancellationToken);
            if (userExist is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 3 - check if the new email is already exist
            var isEmailExist = await repo.IsEmailExistAsync(email: command.User.Email, excludeUserId: userExist.Id.Value, cancellationToken: cancellationToken);
            if (isEmailExist)
            {
                throw new BadRequestException("Email is already exist.");
            }

            // 3 - Update system user
            userExist.Update(firstName: command.User.FirstName,
                             lastName: command.User.LastName,
                             email: command.User.Email);

            // 4 - Update identity user using identity service and save changes
            var identityResult = await identityService.UpdateUserAsync(userId: userExist.Id.Value.ToString(),
                                                               userName: userExist.UserName,
                                                               email: userExist.Email,
                                                               password: command.User.Password);

            // 4 - Check if the user updated successfully
            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.First().Description);
            }

            // 5 - Build and return the response
            var response = EGResponseFactory.Success<Guid>(userExist.Id.Value, "Success operation.");

            return new UpdateSubUserResult(response);
        }
    }
}
