using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;

namespace EGHeals.Application.Features.Users.Commands.UpdateSubUser
{
    public class UpdateSubUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateSubUserCommand, UpdateSubUserResult>
    {
        public async Task<UpdateSubUserResult> Handle(UpdateSubUserCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist
            var userExist = await repo.GetByIdAsync(id: UserId.Of(command.Id),
                                                    includeOwnershipId: true,
                                                    cancellationToken: cancellationToken);
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

            // 4 - Update system user
            userExist.Update(firstName: command.User.FirstName,
                             lastName: command.User.LastName,
                             email: command.User.Email);

            // 5 - Update user
            var updatedUser = await repo.UpdateAsync(userExist);
            if (updatedUser is null)
            {
                throw new BadRequestException("User not found");
            }

            // 6 - Save changes
            await unitOfWork.SaveChangesAsync();

            // 7 - Build and return the response
            var response = EGResponseFactory.Success<Guid>(userExist.Id.Value, "Success operation.");

            return new UpdateSubUserResult(response);
        }
    }
}
