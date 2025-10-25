using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Application.Contracts.Roles;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Repositories.Roles
{
    public class RoleRepository<TContext> : BaseRepository<Role, RoleId, TContext>, IRoleRepository where TContext : DbContext
    {
        public RoleRepository(TContext dbContext, ICurrentUserService userContext) : base(dbContext, userContext) { }

        public async Task<IEnumerable<Role>> GetRolesAsync(UserActivity? type, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsQueryable()
                               .AsNoTracking()
                               .Include(x => x.Permissions)
                                    .ThenInclude(x => x.Permission)
                               .Where(x => x.UserActivity == type && x.IsActive && !x.IsAdmin && !x.IsDeleted)
                               .ToListAsync(cancellationToken);

        }
    }
}
