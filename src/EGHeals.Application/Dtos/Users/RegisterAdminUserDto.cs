
namespace EGHeals.Application.Dtos.Users
{
    public record RegisterAdminUserDto(string FirstName,
                                       string LastName,
                                       string UserName,
                                       string? Email, 
                                       string? Mobile,
                                       string Password,
                                       string NationalId,
                                       Gender Gender,
                                       UserActivity UserActivity,
                                       string ActivityDescription) : RegisterUserBaseDto(FirstName, LastName, UserName, Email, Mobile, Password);
}
