using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Contracts.Users
{
    namespace EGHeals.Application.Contracts.Users
    {
        public interface IUserRepository : IBaseRepository<SystemUser, SystemUserId>
        {
            Task<bool> IsUserExistAsync(string username, CancellationToken cancellationToken = default);

            Task<bool> IsEmailExistAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default);

            Task<bool> IsMobileExistAsync(string mobile, Guid? excludeUserId = null, CancellationToken cancellationToken = default);

            Task<SystemUser?> GetUserRolesAsync(string username, CancellationToken cancellationToken = default);

            Task<IEnumerable<SystemUser>> GetSubUsersAsync(QueryOptions<SystemUser> options,
                                                           bool ignoreOwnership = false,
                                                           CancellationToken cancellationToken = default);

            Task<long> GetSubUsersCountAsync(QueryFilters<SystemUser> filters,
                                             bool ignoreOwnership = false,
                                             CancellationToken cancellationToken = default);

            Task<SystemUser?> GetSubUserRolesAsync(Guid userId,
                                                   bool ignoreOwnership = false,
                                                   CancellationToken cancellationToken = default);
        }
    }
}
