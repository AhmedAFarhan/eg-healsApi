using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;


namespace EGHeals.Infrastructure.Repositories.Shared.Users
{
    public class UserRepository(ApplicationIdentityDbContext dbContext,
                                UserManager<AppUser> userManager, 
                                IUserContextService userContext) : IUserRepository
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

        public async Task<User?> FindByNameAsync(string username, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpperInvariant() && !u.IsDeleted, cancellationToken);
            return user?.ToDomainUser();
        }
        public async Task<User?> FindByNameWithRolesAsync(string username, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.Include(x => x.UserRoles)
                                                   .ThenInclude(x => x.Role)
                                                        .ThenInclude(x => x.RolePermissions)
                                                             .ThenInclude(x => x.Permission)
                                            .Include(x => x.Tenant)
                                            .FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpperInvariant() && !u.IsDeleted, cancellationToken);
            return user?.ToDomainUser();
        }

        public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpperInvariant() && !u.IsDeleted, cancellationToken);
            return user?.ToDomainUser();
        }
        public async Task<User?> FindByMobileAsync(string mobile, CancellationToken cancellationToken = default)
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
        public async Task<User?> UpdateAsync(User entity, bool includeRoles = false, CancellationToken cancellationToken = default)
        {            
            var existingUser = includeRoles ? await dbContext.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(u => u.Id == entity.Id, cancellationToken)
                                            : await dbContext.Users.FirstOrDefaultAsync(u => u.Id == entity.Id, cancellationToken);

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

        public async Task<User?> GetByIdAsync(UserId id,
                                              bool includeRoles = false,
                                              CancellationToken cancellationToken = default)
        {
            var query = dbContext.Users.AsQueryable();

            if (includeRoles)
            {
                query = query.Include(x => x.UserRoles).ThenInclude(x => x.Role);
            }

            query = query.Where(u => u.Id == id && !u.IsDeleted);

            var identityUser = await query.FirstOrDefaultAsync(cancellationToken);

            return identityUser?.ToDomainUser();
        }

        public async Task<IEnumerable<User>> GetAllAsync(QueryOptions<User> options,
                                                         bool includeRoles = false,
                                                         CancellationToken cancellationToken = default)
        {
            var adaptedOptions = options.ToAppUserQueryOptions();

            //Starting query
            var query = dbContext.Users.Include(u => u.UserRoles)
                                            .ThenInclude(up => up.Role)
                                       .Where(x => !x.IsDeleted && x.Id != UserId.Of(userContext.UserId));

            // Apply filtering
            var filterExpression = adaptedOptions.QueryFilters.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            // Apply sorting
            query = adaptedOptions.ApplySorting(query);

            // Apply pagination
            query = query.Skip(adaptedOptions.Skip).Take(adaptedOptions.Take);

            // Get users
            return await query.AsNoTracking().AsSplitQuery().Select(x => x.ToDomainUser()).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(QueryFilters<User> filters,
                                              CancellationToken cancellationToken = default)
        {
            var adaptedFilter = filters.ToAppUserQueryFilters();

            //Starting query
            var query = dbContext.Users.Where(x => !x.IsDeleted && x.Id != UserId.Of(userContext.UserId));

            // Apply filtering
            var filterExpression = adaptedFilter.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            return await query.LongCountAsync(cancellationToken);
        }

    }
}
