using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserRoleId.Of(dbId));

            builder.Property(x => x.OwnershipId).HasConversion(id => id.Value, dbId => SystemUserId.Of(dbId));

            builder.Property(x => x.SystemUserId).HasConversion(id => id.Value, dbId => SystemUserId.Of(dbId));

            builder.Property(x => x.RoleId).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.HasIndex(x => new { x.SystemUserId, x.RoleId }).IsUnique();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.UserRolePermissions).WithOne().HasForeignKey(tb => tb.UserRoleId).OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict); // incase we don't need navigation property
            builder.HasOne(u => u.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<SystemUser>().WithMany().HasForeignKey(x => x.OwnershipId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
