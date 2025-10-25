using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class PermissionTranslationConfiguration : IEntityTypeConfiguration<PermissionTranslation>
    {
        public void Configure(EntityTypeBuilder<PermissionTranslation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => PermissionTranslationId.Of(dbId));

            builder.Property(x => x.PermissionId).HasConversion(id => id.Value, dbId => PermissionId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150);

            builder.HasIndex(x => new { x.PermissionId, x.LanguageCode }).IsUnique();

            builder.Property(x => x.LanguageCode).HasDefaultValue(LanguageCode.ENGLISH).HasConversion(enums => enums.ToString(), dbEnums => (LanguageCode)Enum.Parse(typeof(LanguageCode), dbEnums));

            /*************************** Relationships ****************************/
        }
    }
}
