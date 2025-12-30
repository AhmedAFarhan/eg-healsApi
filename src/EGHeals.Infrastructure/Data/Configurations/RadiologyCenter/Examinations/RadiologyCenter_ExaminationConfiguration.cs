using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.Models.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;

namespace EGHeals.Infrastructure.Data.Configurations.RadiologyCenter.Examinations
{
    public class RadiologyCenter_ExaminationConfiguration : IEntityTypeConfiguration<RadiologyCenter_Examination>
    {
        public void Configure(EntityTypeBuilder<RadiologyCenter_Examination> builder)
        {
            builder.ToTable("Examinations", "RadiologyCenter");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(id => id.Value, dbId => RadiologyCenter_ExaminationId.Of(dbId));

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.Property(x => x.Device).HasDefaultValue(RadiologyCenetr_Device.DEVICE1).HasConversion(enums => enums.ToString(), dbEnums => (RadiologyCenetr_Device)Enum.Parse(typeof(RadiologyCenetr_Device), dbEnums));

            builder.HasIndex(x => new { x.Name, x.Device }).IsUnique();

            /*************************** Relationships ****************************/
        }
    }
}
