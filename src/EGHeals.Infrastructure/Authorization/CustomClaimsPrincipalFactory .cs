using EGHeals.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EGHeals.Infrastructure.Authorization
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        private readonly ApplicationIdentityDbContext _db;

        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager,
                                            IOptions<IdentityOptions> options,
                                            ApplicationIdentityDbContext db) : base(userManager, options)
        {
            _db = db;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var permissions = await _db.Users.Where(u => u.Id == user.Id)
                                            .SelectMany(u => u.UserPermissions
                                                .Where(up => up.Permission.IsActive)
                                                .Select(up => up.Permission))
                                            .ToListAsync();

            foreach (var p in permissions) identity.AddClaim(new Claim("Permission", p.Name));

            return identity;
        }
    }
}
