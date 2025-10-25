using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services;

namespace EGHeals.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork,
                                    IJwtService jwtService,
                                    IIdentityService identityService) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create User repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist (Not deleted or deactivated)
            var isUserExist = await repo.IsUserExistAsync(command.UserLogin.Username, cancellationToken);
            if (!isUserExist)
            {
                throw new BadRequestException("Incorrect username or password.");
            }

            // 3 - Check if the password is correct (using identity)
            var validCred = await identityService.CheckPasswordAsync(command.UserLogin.Username, command.UserLogin.Password);
            if (!validCred)
            {
                throw new BadRequestException("Incorrect username or password.");
            }

            // 4 - GET user Roles
            var user = await repo.GetUserRolesAsync(command.UserLogin.Username, cancellationToken);
            if (user is null)
            {
                throw new BadRequestException("User no longer exist.");
            }

            // 4 - Generate secured token
            string token = jwtService.GenerateToken(user);

            var response = EGResponseFactory.Success<string>(token, "Success operation.");

            return new LoginResult(response);
        }
    }
}
