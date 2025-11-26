using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Application.Features.Shared.Users.Commands.Deactivate
{
    public class DeactivateUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeactivateUserCommand, DeactivateUserResult>
    {
        public async Task<DeactivateUserResult> Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
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

            // 3 - Deactivate current user
            existingUser.Deactivate();

            // 4 - Update user
            var deactivatedUser = await repo.UpdateAsync(existingUser);
            if (deactivatedUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 5 - Save changes
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 6 - Build and return the response
            var response = EGResponseFactory.Success(deactivatedUser.Id.Value, "Success operation.");

            return new DeactivateUserResult(response);
        }
    }
}
