using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyCenter_ExaminationConsumablePreference : AuditableEntity<RadiologyCenter_ExaminationConsumablePreferenceId>
    {
        internal RadiologyCenter_ExaminationConsumablePreference(RadiologyCenter_ExaminationPreferenceId examinationPreferenceId, RadiologyCenter_ProductId radiologyItemId, decimal qty)
        {
            Id = RadiologyCenter_ExaminationConsumablePreferenceId.Of(Guid.NewGuid());
            ExaminationPreferenceId = examinationPreferenceId;
            RadiologyItemId = radiologyItemId;
            Qty = qty;
        }

        public RadiologyCenter_ExaminationPreferenceId ExaminationPreferenceId { get; private set; } = default!;
        public RadiologyCenter_ProductId RadiologyItemId { get; private set; } = default!;
        public decimal Qty { get; private set; } = default!;

        public void Update(decimal qty) => Qty = qty;
    }
}
