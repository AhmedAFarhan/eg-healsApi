using EGHeals.Application.Services.Users;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Extensions;

namespace EGHeals.Infrastructure.Services.Users
{
    public class UserCommandService(ApplicationIdentityDbContext dbContext) : IUserCommandService
    {
        public async Task<User?> ActivateAsync(User user, CancellationToken cancellationToken = default)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

            if (existingUser is null) return null;

            existingUser.IsActive = true;

            dbContext.Users.Update(existingUser);

            return existingUser.ToDomainUser();
        }
        public async Task<User?> DeactivateAsync(User user, CancellationToken cancellationToken = default)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

            if (existingUser is null) return null;

            existingUser.IsActive = false;

            dbContext.Users.Update(existingUser);

            return existingUser.ToDomainUser();
        }
    }
}
