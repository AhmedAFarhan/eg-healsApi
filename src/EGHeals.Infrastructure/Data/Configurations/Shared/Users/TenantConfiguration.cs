using EGHeals.Domain.Enums.Shared;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants", "Shared");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => TenantId.Of(dbId));

            builder.Property(x => x.NationalId).HasMaxLength(14).IsRequired();

            builder.Property(x => x.ActivityDescription).HasMaxLength(250).IsRequired();

            builder.Property(x => x.Gender).HasDefaultValue(Gender.MALE).HasConversion(enums => enums.ToString(), dbEnums => (Gender)Enum.Parse(typeof(Gender), dbEnums));

            builder.Property(x => x.UserActivity).HasDefaultValue(UserActivity.SYSTEM).HasConversion(enums => enums.ToString(), dbEnums => (UserActivity)Enum.Parse(typeof(UserActivity), dbEnums));

            /*************************** Relationships ****************************/
        }
    }
}
