using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserPermissionId.Of(dbId));

            builder.Property(x => x.OwnershipId).HasConversion(id => id.Value, dbId => UserId.Of(dbId));

            builder.Property(x => x.UserId).HasConversion(id => id.Value, dbId => UserId.Of(dbId));

            builder.Property(x => x.PermissionId).HasConversion(id => id.Value, dbId => PermissionId.Of(dbId));

            builder.HasIndex(x => new { x.UserId, x.PermissionId }).IsUnique();

            /*************************** Relationships ****************************/
            builder.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(u => u.Permission).WithMany().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AppUser>().WithMany().HasForeignKey(x => x.OwnershipId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
