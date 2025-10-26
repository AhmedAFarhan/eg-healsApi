using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using BuildingBlocks.Domain.ValueObjects;
using BuildingBlocks.Exceptions;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Dtos.Users.Requests;
using EGHeals.Application.Services;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using EGHeals.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace EGHeals.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(UserManager<ApplicationUser> userManager,
                               IUserRepository userRepository,
                               IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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
        public async Task<IdentityResult> CreateUserAsync(Guid userId,
                                                          string firstName,
                                                          string lastName,
                                                          string email,
                                                          string password,
                                                          IEnumerable<UserRoleRequestDto> UserRoles,
                                                          string? mobile = null)
        {
            // 1 - Create transaction
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            // 2 - Create system user
            var systemUserId = SystemUserId.Of(userId);
            var systemUser = SystemUser.Create(id: systemUserId,
                                               firstName: firstName,
                                               lastName: lastName,
                                               email: email);

            // 3 - Create identity user
            var identityUser = new ApplicationUser
            {
                Id = systemUserId.Value,
                UserName = systemUser.UserName,
                Email = systemUser.Email,
                PhoneNumber = systemUser.Mobile,
            };

            // 4 - Add user roles
            foreach(var role in UserRoles.ToList())
            {
                var createdRole = systemUser.AddUserRole(RoleId.Of(role.RoleId));
                foreach(var permission in role.RolePermissions)
                {
                    createdRole.AddPermission(RolePermissionId.Of(permission.RolePermissionId));
                }
            }

            // 5 - Add domain user
            await _userRepository.AddOneAsync(systemUser);

            // 6 - Save domain user
            await _unitOfWork.SaveChangesAsync();

            // 7 - Add identity user
            var identityResult = await _userManager.CreateAsync(identityUser, password);

            // 8 - Check if the identity user created successfully
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            // 9 - Complete transaction
            transactionScope.Complete();

            // 10 - return identity result
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId,
                                                          string userName,
                                                          string email,
                                                          string password,
                                                          string? mobile = null)
        {
            // 1 - Create transaction
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            // 2 - Update user main info
            user.UserName = userName;
            user.Email = email;
            user.PhoneNumber = mobile;

            var identityResult = await _userManager.UpdateAsync(user);

            // 8 - Check if the identity user updated successfully
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            // 3 - Update user password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await _userManager.ResetPasswordAsync(user, token, password);

            // 8 - Check if the identity user password updated successfully
            if (!passwordResult.Succeeded)
            {
                return passwordResult;
            }

            // 6 - Save domain user
            await _unitOfWork.SaveChangesAsync();

            // 9 - Complete transaction
            transactionScope.Complete();

            // 10 - return identity result
            return IdentityResult.Success;
        }

        public Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }        
    }
}
