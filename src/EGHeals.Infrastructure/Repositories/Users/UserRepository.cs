using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Infrastructure.Repositories.Users
{
    public class UserRepository<TContext> : BaseRepository<SystemUser, SystemUserId, TContext>, IUserRepository where TContext : DbContext
    {
        public UserRepository(TContext dbContext, ICurrentUserService userContext) : base(dbContext, userContext) { }

        public async Task<bool> IsUserExistAsync(string username, CancellationToken cancellationToken = default)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.UserName == username && u.IsActive && !u.IsDeleted);

            return user is not null;
        }

        public async Task<SystemUser?> GetUserRolesAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                               .AsSplitQuery()
                               .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.Role)
                               .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.UserRolePermissions)
                                        .ThenInclude(x => x.RolePermission)
                                            .ThenInclude(x => x.Permission)
                               .FirstOrDefaultAsync(u => u.UserName == username && u.IsActive && !u.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<SystemUser>> GetSubUsersAsync(QueryOptions<SystemUser> options,
                                                                    bool ignoreOwnership = false,
                                                                    CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = _dbSet.AsQueryable().Where(x => !x.IsDeleted && x.IsActive);

            //Apply Ownership
            query = await ApplyOwnership(query, ignoreOwnership);

            query = query.Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role);

            // Apply filtering
            var filterExpression = options.QueryFilters.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            // Apply sorting
            query = options.ApplySorting(query);

            // Apply pagination
            query = query.Skip(options.Skip).Take(options.Take);


            return await query.AsNoTracking().AsSplitQuery().ToListAsync(cancellationToken);
        }

        public async Task<long> GetSubUsersCountAsync(QueryFilters<SystemUser> filters,
                                                      bool ignoreOwnership = false,
                                                      CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = _dbSet.AsQueryable().Where(x => !x.IsDeleted /*&& x.UserType == UserType.SUBUSER*/);

            //Apply Ownership
            query = await ApplyOwnership(query, ignoreOwnership);

            // Apply filtering
            var filterExpression = filters.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            return await query.LongCountAsync(cancellationToken);
        }

        public async Task<SystemUser?> GetSubUserRolesAsync(Guid userId,
                                                            bool ignoreOwnership = false,
                                                            CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = _dbSet.AsQueryable().Where(x => !x.IsDeleted /*&& x.UserType == UserType.SUBUSER*/);

            //Apply Ownership
            query = await ApplyOwnership(query, ignoreOwnership);

            return await _dbSet.AsQueryable().AsNoTracking()
                                             .AsSplitQuery().
                                             Include(x => x.UserRoles)
                                                .ThenInclude(x => x.Role)
                                            .Include(x => x.UserRoles)
                                                .ThenInclude(x => x.UserRolePermissions)
                                                    .ThenInclude(x => x.RolePermission)
                                                        .ThenInclude(x => x.Permission)
                                            .FirstOrDefaultAsync(x => x.Id == SystemUserId.Of(userId));

        }
    }
}
