using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Applications
{
    internal class UserClientApplicationConfiguration : IEntityTypeConfiguration<UserClientApplication>
    {
        public void Configure(EntityTypeBuilder<UserClientApplication> builder)
        {
            builder.ToTable("UserClientApplications", "Shared");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserClientApplicationId.Of(dbId));

            builder.Property(x => x.UserId).HasConversion(id => id.Value, dbId => UserId.Of(dbId));

            builder.Property(x => x.ClientApplicationId).HasConversion(id => id.Value, dbId => ClientApplicationId.Of(dbId));

            builder.HasIndex(x => new { x.ClientApplicationId, x.UserId }).IsUnique();

            /*************************** Relationships ****************************/
        }
    }
}
