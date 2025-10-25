using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class RoleTranslationConfiguration : IEntityTypeConfiguration<RoleTranslation>
    {
        public void Configure(EntityTypeBuilder<RoleTranslation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RoleTranslationId.Of(dbId));

            builder.Property(x => x.RoleId).HasConversion(id => id.Value, dbId => RoleId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150);

            builder.HasIndex(x => new { x.RoleId, x.LanguageCode }).IsUnique();

            builder.Property(x => x.LanguageCode).HasDefaultValue(LanguageCode.ENGLISH).HasConversion(enums => enums.ToString(), dbEnums => (LanguageCode)Enum.Parse(typeof(LanguageCode), dbEnums));

            /*************************** Relationships ****************************/
        }
    }
}
