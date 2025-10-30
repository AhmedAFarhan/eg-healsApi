using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Application.Extensions.Users;
using EGHeals.Application.Services.Users;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Extensions;

namespace EGHeals.Infrastructure.Services.Users
{
    public class UserQueryService(ApplicationIdentityDbContext dbContext, 
                                  ICurrentUserService currentUserService) : IUserQueryService
    {
        public async Task<UserResponseDto?> GetUserWithPermissions(UserId id, CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.Include(x => x.UserPermissions)
                                                   .ThenInclude(x => x.Permission)
                                            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            var domainUser = user?.ToDomainUser();

            return domainUser?.ToUserDto();
        }

        public async Task<IEnumerable<SubUserResponseDto>> GetSubUsersAsync(QueryOptions<User> options,
                                                                            bool includeOwnership = false,
                                                                            CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = dbContext.Users.Where(x => !x.IsDeleted).Select(x => x.ToDomainUser());

            //Apply Ownership
            if (includeOwnership)
            {
                query = query.Where(x => x.OwnershipId == UserId.Of(currentUserService.OwnershipId.Value));
            }

            //query = query.Include(x => x.UserPermissions);

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

            var subUsers = await query.AsNoTracking().AsSplitQuery().ToListAsync(cancellationToken);

            return subUsers.ToSubUsersDtos();
        }
    }
}
