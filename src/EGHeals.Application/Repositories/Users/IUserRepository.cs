using EGHeals.Domain.Models.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Application.Repositories.Users
{
    namespace EGHeals.Application.Contracts.Users
    {
        public interface IUserRepository
        {
            Task<User?> FindUserByNameAsync(string username, CancellationToken cancellationToken = default);
            Task<bool> IsEmailExistAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
            Task<bool> IsMobileExistAsync(string mobile, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
            Task<bool> CheckPasswordAsync(string username, string password);
            Task<IdentityResult> CreateAsync(User entity, CancellationToken cancellationToken = default);
            Task<User?> UpdateAsync(User entity, CancellationToken cancellationToken = default);
            Task<User?> SoftDeleteAsync(User user, CancellationToken cancellationToken = default);
            Task<User?> HardDeleteAsync(UserId id, CancellationToken cancellationToken = default);
            Task<User?> GetByIdAsync(UserId id, bool includeOwnershipId = false, CancellationToken cancellationToken = default);
            Task<long> GetSubUsersCountAsync(QueryFilters<User> filters,
                                                      bool includeOwnership = false,
                                                      CancellationToken cancellationToken = default);



            //Task<User?> GetUserRolesAsync(string username, CancellationToken cancellationToken = default);

            //Task<IEnumerable<User>> GetSubUsersAsync(QueryOptions<User> options,
            //                                               bool ignoreOwnership = false,
            //                                               CancellationToken cancellationToken = default);

            //Task<long> GetSubUsersCountAsync(QueryFilters<User> filters,
            //                                 bool ignoreOwnership = false,
            //                                 CancellationToken cancellationToken = default);

            //Task<User?> GetSubUserRolesAsync(Guid userId,
            //                                       bool ignoreOwnership = false,
            //                                       CancellationToken cancellationToken = default);
        }
    }
}
