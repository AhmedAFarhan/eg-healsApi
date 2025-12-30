using EGHeals.Domain.Models.RadiologyCenter.Examinations;
using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
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
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<PermissionTranslation> PermissionTranslations => Set<PermissionTranslation>();

        public DbSet<RadiologyCenter_Examination> RadiologyCenter_Examinations => Set<RadiologyCenter_Examination>();
        public DbSet<RadiologyCenter_ExaminationCost> RadiologyCenter_ExaminationCost => Set<RadiologyCenter_ExaminationCost>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
