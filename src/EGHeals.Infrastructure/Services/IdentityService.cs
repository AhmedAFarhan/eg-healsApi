using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services;
using EGHeals.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace EGHeals.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public IdentityService(UserManager<ApplicationUser> userManager,
                               IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;            
        }

        public Task<string?> GetUserIdAsync(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> CheckPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return false;

            var IsValid = await _userManager.CheckPasswordAsync(user, password);

            return IsValid;
        } 
        public Task<IdentityResult> CreateUserAsync(string username, string? email, string? mobile, string password)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            throw new NotImplementedException();
        }
        public Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public async Task<IdentityResult> SignInAsync(string username, string password)
        {
            // 1 - Check if the user exist
             

            var user = await _userManager.FindByEmailAsync(username);

            //// If not found by email, try by mobile
            //if (user == null)
            //{
            //    user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == username);
            //}

            //var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            throw new NotImplementedException();
        }

        
    }
}
