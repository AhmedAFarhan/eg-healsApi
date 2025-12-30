using EGHeals.Domain.Models.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;

namespace EGHeals.Infrastructure.Data.Configurations.RadiologyCenter.Examinations
{
    public class RadiologyCenter_ExaminationCostConfiguration: IEntityTypeConfiguration<RadiologyCenter_ExaminationCost>
    {
        private readonly IUserContextService _userContext;

        public RadiologyCenter_ExaminationCostConfiguration(IUserContextService userContext)
        {
            _userContext = userContext;
        }
        public RadiologyCenter_ExaminationCostConfiguration() { } // <— required for migrations

        public void Configure(EntityTypeBuilder<RadiologyCenter_ExaminationCost> builder)
        {
            builder.ToTable("ExaminationCosts", "RadiologyCenter");

            builder.HasQueryFilter(x => _userContext.IsSystemUser || x.TenantId == TenantId.Of(_userContext.TenantId));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RadiologyCenter_ExaminationCostId.Of(dbId));

            builder.Property(x => x.TenantId).HasConversion(id => id.Value, dbId => TenantId.Of(dbId));

            builder.Property(x => x.ExaminationId).HasConversion(id => id.Value, dbId => RadiologyCenter_ExaminationId.Of(dbId));

            builder.HasIndex(x => new { x.ExaminationId, x.TenantId }).IsUnique();

            /*************************** Relationships ****************************/

            builder.HasOne(x => x.Examination).WithMany().HasForeignKey(x => x.ExaminationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
