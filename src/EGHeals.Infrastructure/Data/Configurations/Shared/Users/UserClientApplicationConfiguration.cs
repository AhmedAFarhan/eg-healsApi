using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    internal class UserClientApplicationConfiguration : IEntityTypeConfiguration<UserClientApplication>
    {
        public void Configure(EntityTypeBuilder<UserClientApplication> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserClientApplicationId.Of(dbId));

            builder.Property(x => x.SystemUserId).HasConversion(id => id.Value, dbId => SystemUserId.Of(dbId));

            builder.Property(x => x.ClientApplicationId).HasConversion(id => id.Value, dbId => ClientApplicationId.Of(dbId));

            builder.HasIndex(x => new { x.ClientApplicationId, x.SystemUserId }).IsUnique();

            /*************************** Relationships ****************************/
        }
    }
}
