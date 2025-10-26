using EGHeals.Application.Dtos.Users.Requests;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Application.Services
{
    public interface IIdentityService
    {
        Task<string?> GetUserIdAsync(string username);
        Task<bool> CheckPasswordAsync(string username, string password);
        Task<IdentityResult> CreateUserAsync(Guid userId,
                                            string firstName,
                                            string lastName,
                                            string email,
                                            string password,
                                            IEnumerable<UserRoleRequestDto> UserRoles,
                                            string? mobile = null);

        Task<IdentityResult> UpdateUserAsync(string userId,
                                             string userName,
                                             string email,
                                             string password,
                                             string? mobile = null);

        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
