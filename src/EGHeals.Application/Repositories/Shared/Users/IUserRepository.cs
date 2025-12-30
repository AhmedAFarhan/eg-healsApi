using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Application.Repositories.Users
{
    namespace EGHeals.Application.Contracts.Users
    {
        public interface IUserRepository
        {
            Task<User?> FindByNameAsync(string username, CancellationToken cancellationToken = default);
            Task<User?> FindByNameWithRolesAsync(string username, CancellationToken cancellationToken = default);

            Task<bool> IsEmailExistAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
            Task<bool> IsMobileExistAsync(string mobile, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
            Task<bool> CheckPasswordAsync(string username, string password);

            Task<IdentityResult> CreateAsync(User entity, CancellationToken cancellationToken = default);
            Task<User?> UpdateAsync(User entity, bool includeRoles = false, CancellationToken cancellationToken = default);
            Task<User?> DeleteAsync(UserId id, CancellationToken cancellationToken = default);
            Task<User?> GetByIdAsync(UserId id,
                                    bool includeRoles = false,
                                    CancellationToken cancellationToken = default);

            Task<IEnumerable<User>> GetAllAsync(QueryOptions<User> options,
                                                         bool includeRoles = false,
                                                         CancellationToken cancellationToken = default);

            Task<long> GetCountAsync(QueryFilters<User> filters,
                                             CancellationToken cancellationToken = default);
        } 
    }
}
