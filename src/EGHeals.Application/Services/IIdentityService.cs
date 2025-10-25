using Microsoft.AspNetCore.Identity;

namespace EGHeals.Application.Services
{
    public interface IIdentityService
    {
        Task<string?> GetUserIdAsync(string username);
        Task<bool> CheckPasswordAsync(string username, string password);
        Task<IdentityResult> CreateUserAsync(string username, string? email, string? mobile, string password);
        Task<IdentityResult> SignInAsync(string username, string password);
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
