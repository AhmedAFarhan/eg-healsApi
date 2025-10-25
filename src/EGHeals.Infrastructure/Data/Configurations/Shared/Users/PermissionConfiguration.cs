using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => PermissionId.Of(dbId));

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.Translations).WithOne().HasForeignKey(tb => tb.PermissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
