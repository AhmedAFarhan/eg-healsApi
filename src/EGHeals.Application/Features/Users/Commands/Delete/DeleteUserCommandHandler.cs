using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;

namespace EGHeals.Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
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

            // 3 - Delete current user
            var deletedUser = await repo.SoftDeleteAsync(existingUser, cancellationToken);
            if (deletedUser is null)
            {
                throw new BadRequestException("User not found.");
            }

            // 4 - Save changes
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 5 - Build and return the response
            var response = EGResponseFactory.Success<Guid>(deletedUser.Id.Value, "Success operation.");

            return new DeleteUserResult(response);
        }
    }
}
