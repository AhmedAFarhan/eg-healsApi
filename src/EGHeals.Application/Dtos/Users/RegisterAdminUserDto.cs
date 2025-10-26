
namespace EGHeals.Application.Dtos.Users
{
    public record RegisterAdminUserDto(string FirstName,
                                       string LastName,
                                       string Email, 
                                       string? Mobile,
                                       string Password,
                                       string NationalId,
                                       Gender Gender,
                                       UserActivity UserActivity,
                                       string ActivityDescription) : RegisterUserBaseDto(FirstName, LastName, Email, Password);
}
