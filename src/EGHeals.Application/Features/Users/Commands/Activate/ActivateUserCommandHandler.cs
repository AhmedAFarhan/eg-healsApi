using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Features.Users.Commands.Delete;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Users;

namespace EGHeals.Application.Features.Users.Commands.Activate
{
    public class ActivateUserCommandHandler(IUnitOfWork unitOfWork,
                                            IUserCommandService userCommandService) : ICommandHandler<ActivateUserCommand, ActivateUserResult>
    {
        public async Task<ActivateUserResult> Handle(ActivateUserCommand command, CancellationToken cancellationToken)
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

            // 3 - Activate current user
            var activatedUser = await userCommandService.ActivateAsync(existingUser, cancellationToken);
            if (activatedUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 4 - Save changes
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 5 - Build and return the response
            var response = EGResponseFactory.Success<Guid>(activatedUser.Id.Value, "Success operation.");

            return new ActivateUserResult(response);
        }
    }
}
