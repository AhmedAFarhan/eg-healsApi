using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.Property(x => x.UserActivity).IsRequired(false);

            //builder.Property(x => x.UserActivity).HasDefaultValue(UserActivity.RADIOLOGY).HasConversion(enums => enums.ToString(), dbEnums => (UserActivity)Enum.Parse(typeof(UserActivity), dbEnums));

            builder.Property(x => x.UserActivity).HasDefaultValue(UserActivity.RADIOLOGY).HasConversion(enums => enums.HasValue ? enums.Value.ToString() : null,dbEnums => dbEnums != null? Enum.Parse<UserActivity>(dbEnums): (UserActivity?)null);

            builder.HasIndex(x => new { x.Name , x.UserActivity }).IsUnique();

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.Permissions).WithOne().HasForeignKey(tb => tb.RoleId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Translations).WithOne().HasForeignKey(tb => tb.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
