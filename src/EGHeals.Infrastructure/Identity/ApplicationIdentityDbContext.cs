using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EGHeals.Infrastructure.Identity
{
    public class ApplicationIdentityDbContext : IdentityUserContext<ApplicationUser, Guid>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options) { }
    }
}
