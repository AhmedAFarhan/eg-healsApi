using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Applications
{
    internal class ClientApplicationConfiguration : IEntityTypeConfiguration<ClientApplication>
    {
        public void Configure(EntityTypeBuilder<ClientApplication> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => ClientApplicationId.Of(dbId));

            builder.HasIndex(x => x.Client).IsUnique();
            builder.Property(x => x.Client).HasMaxLength(150).IsRequired();

            /*************************** Relationships ****************************/
        }
    }
}
