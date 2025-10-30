using EGHeals.Application.Dtos.Users.Requests;

namespace EGHeals.Application.Features.Users.Commands.Login
{
    public record LoginCommand(LoginUserRequestDto UserLogin) : ICommand<LoginResult>;
    public record LoginResult(EGResponse<string> Response);

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserLogin).NotNull().WithMessage("1")
                                     .SetValidator(new UserLoginDtoValidator());
        }
    }
    public class UserLoginDtoValidator : AbstractValidator<LoginUserRequestDto>
    {
        public UserLoginDtoValidator()
        {

            RuleFor(x => x.Username).NotNull().WithMessage("2")
                                    .NotEmpty().WithMessage("3")
                                    .NoWhitespacesAllowed("4")
                                    .MaximumLength(150).WithMessage("5")
                                    .MinimumLength(3).WithMessage("6");

            RuleFor(x => x.Password).NotNull().WithMessage("7")
                                    .NotEmpty().WithMessage("8")
                                    .NoWhitespacesAllowed("9")
                                    .MaximumLength(50).WithMessage("10")
                                    .MinimumLength(3).WithMessage("11");
        }
    }
}
