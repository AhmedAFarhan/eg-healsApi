using EGHeals.Domain.Models.Shared.Owners;
using EGHeals.Domain.Models.Shared.Users;
using System.Reflection;

namespace EGHeals.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public bool IsSeeding { get; set; } = false;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ClientApplication> ClientApplications => Set<ClientApplication>();
        public DbSet<UserClientApplication> UserClientApplications => Set<UserClientApplication>();
        public DbSet<SystemUser> SystemUsers => Set<SystemUser>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<UserRolePermission> UserRolePermissions => Set<UserRolePermission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
