using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Jwt;

namespace EGHeals.Application.Features.Shared.Users.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork,
                                    IJwtService jwtService) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create User repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist (Not deleted)
            var existingUser = await repo.FindByNameWithRolesAsync(command.UserLogin.Username, cancellationToken);
            if (existingUser is null)
            {
                throw new BadRequestException("Incorrect username or password.");
            }

            // 3 - Check if the password is correct (using identity)
            var validCred = await repo.CheckPasswordAsync(command.UserLogin.Username, command.UserLogin.Password);
            if (!validCred)
            {
                throw new BadRequestException("Incorrect username or password.");
            }

            // 4 - Check if the user exist (deactivated)            
            if (!existingUser.IsActive)
            {
                throw new BadRequestException("User is locked.");
            }

            // 6 - Generate secured token
            string token = jwtService.GenerateToken(existingUser);

            var response = EGResponseFactory.Success(token, "Success operation.");

            return new LoginResult(response);
        }
    }
}
