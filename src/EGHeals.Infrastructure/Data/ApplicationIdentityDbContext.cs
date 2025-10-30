using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.Models.Shared.Owners;
using EGHeals.Domain.Models.Shared.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace EGHeals.Infrastructure.Data
{
    public class ApplicationIdentityDbContext : IdentityUserContext<AppUser, UserId>
    {
        public bool IsSeeding { get; set; } = false;

        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options) { }

        public DbSet<ClientApplication> ClientApplications => Set<ClientApplication>();
        public DbSet<UserClientApplication> UserClientApplications => Set<UserClientApplication>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserPermission> UserPermissions => Set<UserPermission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
