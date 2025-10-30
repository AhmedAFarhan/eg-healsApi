using EGHeals.Domain.Models.Shared.Owners;
using EGHeals.Domain.ValueObjects.Shared.Owners;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Owners
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => OwnerId.Of(dbId));

            builder.Property(x => x.NationalId).HasMaxLength(14).IsRequired();

            builder.Property(x => x.ActivityDescription).HasMaxLength(250).IsRequired();

            builder.Property(x => x.Gender).HasDefaultValue(Gender.MALE).HasConversion(enums => enums.ToString(), dbEnums => (Gender)Enum.Parse(typeof(Gender), dbEnums));

            builder.Property(x => x.UserActivity).HasDefaultValue(UserActivity.RADIOLOGY).HasConversion(enums => enums.ToString(), dbEnums => (UserActivity)Enum.Parse(typeof(UserActivity), dbEnums));

            /*************************** Relationships ****************************/

            builder.HasOne<AppUser>().WithMany().HasForeignKey(x => x.OwnershipId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
