using EGHeals.Domain.Enums.Shared;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "Shared");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => PermissionId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.Property(x => x.UserActivity).HasDefaultValue(UserActivity.SYSTEM).HasConversion(enums => enums.ToString(), dbEnums => (UserActivity)Enum.Parse(typeof(UserActivity), dbEnums));

            builder.HasIndex(x => new { x.Name, x.UserActivity }).IsUnique();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.Translations).WithOne().HasForeignKey(tb => tb.PermissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
