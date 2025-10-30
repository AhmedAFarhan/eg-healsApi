using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Extensions;
using EGHeals.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.Repositories.Users
{
    public class UserRepository(ApplicationIdentityDbContext dbContext,
                                UserManager<AppUser> userManager, 
                                ICurrentUserService currentUserService) : IUserRepository
    {
        public async Task<bool> IsEmailExistAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
        {
            var excludedId = excludeUserId.HasValue ? UserId.Of(excludeUserId.Value) : null;
            return await dbContext.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpperInvariant() && (excludedId == null || u.Id != excludedId), cancellationToken);
        }
        public async Task<bool> IsMobileExistAsync(string mobile, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
        {
            var excludedId = excludeUserId.HasValue ? UserId.Of(excludeUserId.Value) : null;
            return await dbContext.Users.AnyAsync(u => u.PhoneNumber == mobile && (excludedId == null || u.Id != excludedId), cancellationToken);
        }
        public async Task<bool> CheckPasswordAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null) return false;

            var IsValid = await userManager.CheckPasswordAsync(user, password);

            return IsValid;
        }

        public async Task<User?> FindUserByNameAsync(string username, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpperInvariant() && !u.IsDeleted, cancellationToken);
            return user?.ToDomainUser();
        }
        public async Task<User?> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpperInvariant() && !u.IsDeleted, cancellationToken);
            return user?.ToDomainUser();
        }
        public async Task<User?> FindUserByMobileAsync(string mobile, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile.ToUpperInvariant() && !u.IsDeleted, cancellationToken);
            return user?.ToDomainUser();
        }
     
        public async Task<IdentityResult> CreateAsync(User entity, CancellationToken cancellationToken = default)
        {
            var identityUser = entity.ToIdentityUser();

            IdentityResult result;

            if (!string.IsNullOrWhiteSpace(entity.RawPassword))
            {
                result = await userManager.CreateAsync(identityUser, entity.RawPassword);
            }
            else
            {
                result = await userManager.CreateAsync(identityUser); // Used with patient users who logged with otp
            }

            if (!result.Succeeded) return result;

            return IdentityResult.Success;
        }
        public async Task<User?> UpdateAsync(User entity, CancellationToken cancellationToken = default)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

            if (existingUser is null) return null;

            existingUser.CopyToIdentity(entity);

            dbContext.Users.Update(existingUser);

            return existingUser.ToDomainUser();
        }
        public async Task<User?> SoftDeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

            if (existingUser is null) return null;

            existingUser.IsDeleted = true;

            dbContext.Users.Update(existingUser);

            return existingUser.ToDomainUser();
        }
        public async Task<User?> HardDeleteAsync(UserId id, CancellationToken cancellationToken = default)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (existingUser is null) return null;

            existingUser.IsDeleted = true;

            dbContext.Users.Remove(existingUser);

            return existingUser.ToDomainUser();
        }
        public async Task<User?> GetByIdAsync(UserId id, bool includeOwnershipId = false, CancellationToken cancellationToken = default)
        {
            var query = dbContext.Users.Where(u => u.Id == id && !u.IsDeleted);

            if (includeOwnershipId)
            {
                query = query.Where(u => u.OwnershipId == UserId.Of(currentUserService.OwnershipId.Value));
            }

            var identityUser = await query.FirstOrDefaultAsync(cancellationToken);

            return identityUser?.ToDomainUser();
        }
        public async Task<long> GetSubUsersCountAsync(QueryFilters<User> filters,
                                                      bool includeOwnership = false,
                                                      CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = dbContext.Users.Where(x => !x.IsDeleted).Select(x => x.ToDomainUser()); ;

            //Apply Ownership
            if(includeOwnership)
            {
                query = query.Where(x => x.OwnershipId == UserId.Of(currentUserService.OwnershipId.Value));
            }

            // Apply filtering
            var filterExpression = filters.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            return await query.LongCountAsync(cancellationToken);
        }
    }
}
