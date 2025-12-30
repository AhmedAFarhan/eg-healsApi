using EGHeals.Domain.Enums.Shared;
using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Applications
{
    internal class ClientApplicationConfiguration : IEntityTypeConfiguration<ClientApplication>
    {
        public void Configure(EntityTypeBuilder<ClientApplication> builder)
        {
            builder.ToTable("ClientApplications", "Shared");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => ClientApplicationId.Of(dbId));

            builder.HasIndex(x => x.ClientId).IsUnique();
            builder.Property(x => x.ClientId).HasMaxLength(150).IsRequired();

            builder.Property(x => x.Platform).HasDefaultValue(Platform.WEB).HasConversion(enums => enums.ToString(), dbEnums => (Platform)Enum.Parse(typeof(Platform), dbEnums));

            /*************************** Relationships ****************************/
        }
    }
}
