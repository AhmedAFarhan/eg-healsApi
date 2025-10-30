using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Jwt;
using EGHeals.Application.Services.Users;

namespace EGHeals.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork,
                                    IUserQueryService userQueryService,
                                    IJwtService jwtService) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // 1 - Create User repo
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            // 2 - Check if the user exist (Not deleted)
            var existingUser = await repo.FindUserByNameAsync(command.UserLogin.Username, cancellationToken);
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

            // 5 - Get user full info with permissions
            var user = await userQueryService.GetUserWithPermissions(existingUser.Id);

            // 6 - Generate secured token
            string token = jwtService.GenerateToken(user);

            var response = EGResponseFactory.Success<string>(token, "Success operation.");

            return new LoginResult(response);
        }
    }
}
