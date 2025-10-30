using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserId.Of(dbId));

            builder.Property(x => x.OwnershipId).HasConversion(id => id.Value, dbId => UserId.Of(dbId));

            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.UserName).HasMaxLength(150).IsRequired();

            builder.HasIndex(x => x.NormalizedUserName).IsUnique();
            builder.Property(x => x.NormalizedUserName).HasMaxLength(150).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired(false);

            builder.HasIndex(x => x.NormalizedEmail).IsUnique();
            builder.Property(x => x.NormalizedEmail).HasMaxLength(150).IsRequired(false);

            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.Property(x => x.PhoneNumber).HasMaxLength(11).IsRequired(false);

            /*************************** Relationships ****************************/

            builder.HasMany(o => o.UserPermissions).WithOne().HasForeignKey(tb => tb.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.UserClientApplications).WithOne().HasForeignKey(tb => tb.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AppUser>().WithMany().HasForeignKey(x => x.OwnershipId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
