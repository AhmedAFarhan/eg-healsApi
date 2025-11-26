using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;


namespace EGHeals.Application.Features.Shared.Users.Commands.RegisterSubUser
{
    public class RegisterSubUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<RegisterSubUserCommand, RegisterUserResult>
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

            // 3 - Create domain user
            var user = User.Create(id: UserId.Of(Guid.NewGuid()),
                                   firstName: command.User.FirstName,
                                   lastName: command.User.LastName,
                                   email: command.User.Email,
                                   rawPassword: command.User.Password);

            // 4 - Add user roles
            foreach(var role in command.User.UserRoles.ToList())
            {
                user.AddRole(RoleId.Of(role.Id));
            }

            // 5 - Add new user with save changes
            var identityResult = await repo.CreateAsync(user);

            // 6 - Check if the user created successfully
            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.First().Description);
            }

            // 7 - Build and return the response
            var response = EGResponseFactory.Success(user.Id.Value, "Success operation.");

            return new RegisterUserResult(response);
        }
    }
}
