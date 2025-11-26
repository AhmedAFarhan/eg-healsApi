using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Infrastructure.Data.Configurations.Shared.Users
{
    public class UserConfiguration/*(IUserContextService userContext)*/ : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users", "Shared");

           // builder.HasQueryFilter(x => userContext.IsSystemUser || x.TenantId == TenantId.Of(userContext.TenantId));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => UserId.Of(dbId));

            builder.Property(x => x.TenantId).HasConversion(id => id.Value, dbId => TenantId.Of(dbId));

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

            builder.HasMany(o => o.UserRoles).WithOne().HasForeignKey(tb => tb.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.UserClientApplications).WithOne().HasForeignKey(tb => tb.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Tenant).WithMany().HasForeignKey(t => t.TenantId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
