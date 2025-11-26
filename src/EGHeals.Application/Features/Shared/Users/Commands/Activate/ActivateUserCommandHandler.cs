using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Application.Features.Shared.Users.Commands.Activate
{
    public class ActivateUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<ActivateUserCommand, ActivateUserResult>
    {
        public async Task<ActivateUserResult> Handle(ActivateUserCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist
            var existingUser = await repo.GetByIdAsync(id: UserId.Of(command.Id),
                                                       cancellationToken: cancellationToken);
            if (existingUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 3 - Activate current user
            existingUser.Activate();

            // 4 - Update user
            var activatedUser = await repo.UpdateAsync(existingUser);
            if (activatedUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 5 - Save changes
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 6 - Build and return the response
            var response = EGResponseFactory.Success(activatedUser.Id.Value, "Success operation.");

            return new ActivateUserResult(response);
        }
    }
}
