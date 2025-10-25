using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RolePermissionId.Of(dbId));

            builder.Property(x => x.RoleId).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.Property(x => x.PermissionId).HasConversion(id => id.Value, dbId => PermissionId.Of(dbId));

            builder.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();

            /*************************** Relationships ****************************/

            //builder.HasOne<Permission>().WithMany().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict); //incase we don't have navigation property
            builder.HasOne(r => r.Permission).WithMany().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
