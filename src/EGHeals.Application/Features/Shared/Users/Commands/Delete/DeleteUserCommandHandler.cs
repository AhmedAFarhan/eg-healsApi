using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Application.Features.Shared.Users.Commands.Delete
{
    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserContextService userContext) : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
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

            // 3 - Soft Delete current user and then update
            existingUser.Delete(userContext.UserId, DateTimeOffset.UtcNow);

            // 4 - Update user
            var deletedUser = await repo.UpdateAsync(existingUser);
            if (deletedUser is null)
            {
                throw new BadRequestException("User not found");
            }

            // 5 - Save changes
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 6 - Build and return the response
            var response = EGResponseFactory.Success(deletedUser.Id.Value, "Success operation.");

            return new DeleteUserResult(response);
        }
    }
}
