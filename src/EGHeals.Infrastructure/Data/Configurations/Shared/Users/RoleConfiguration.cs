using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "Shared");
     
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.Property(x => x.TenantId).HasConversion(id => id.Value, dbId => TenantId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.HasIndex(x => new { x.Name, x.TenantId }).IsUnique();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.RolePermissions).WithOne(rp => rp.Role).HasForeignKey(tb => tb.RoleId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
