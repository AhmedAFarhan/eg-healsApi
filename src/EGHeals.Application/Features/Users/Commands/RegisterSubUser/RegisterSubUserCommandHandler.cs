using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services;


namespace EGHeals.Application.Features.Users.Commands.RegisterSubUser
{
    public class RegisterSubUserCommandHandler(IUnitOfWork unitOfWork,
                                               IIdentityService identityService) : ICommandHandler<RegisterSubUserCommand, RegisterUserResult>
    {
        public async Task<RegisterUserResult> Handle(RegisterSubUserCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the email exist
            var isEmailExist = await repo.IsEmailExistAsync(email: command.User.Email, cancellationToken: cancellationToken);
            if (isEmailExist)
            {
                throw new BadRequestException("Email is already exist.");
            }

            // 3 - Create system user and identity user using identity service
            var userId = Guid.NewGuid();
            var identityResult = await identityService.CreateUserAsync(userId: userId,
                                                                       firstName: command.User.FirstName,
                                                                       lastName : command.User.LastName,
                                                                       email: command.User.Email,
                                                                       password: command.User.Password,
                                                                       UserRoles: command.User.UserRoles);

            // 4 - Check if the user created successfully
            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.First().Description);
            }

            // 5 - Build and return the response
            var response = EGResponseFactory.Success<Guid>(userId, "Success operation.");

            return new RegisterUserResult(response);
        }
    }
}
