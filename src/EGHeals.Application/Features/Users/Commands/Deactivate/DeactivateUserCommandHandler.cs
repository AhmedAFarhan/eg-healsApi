using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Users;

namespace EGHeals.Application.Features.Users.Commands.Deactivate
{
    public class DeactivateUserCommandHandler(IUnitOfWork unitOfWork,
                                             IUserCommandService userCommandService) : ICommandHandler<DeactivateUserCommand, DeactivateUserResult>
    {
        public async Task<DeactivateUserResult> Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist
            var existingUser = await repo.GetByIdAsync(id: UserId.Of(command.Id),
                                                       includeOwnershipId: true,
                                                       cancellationToken: cancellationToken);
            if (existingUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 3 - Deactivate current user
            var deactivatedUser = await userCommandService.DeactivateAsync(existingUser, cancellationToken);
            if (deactivatedUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 4 - Save changes
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 5 - Build and return the response
            var response = EGResponseFactory.Success<Guid>(deactivatedUser.Id.Value, "Success operation.");

            return new DeactivateUserResult(response);
        }
    }
}
