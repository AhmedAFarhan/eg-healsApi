using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyExaminationConsumablePreference : Entity<RadiologyExaminationConsumablePreferenceId>
    {
        internal RadiologyExaminationConsumablePreference(RadiologyExaminationPreferenceId examinationPreferenceId, RadiologyProductId radiologyItemId, decimal qty)
        {
            Id = RadiologyExaminationConsumablePreferenceId.Of(Guid.NewGuid());
            ExaminationPreferenceId = examinationPreferenceId;
            RadiologyItemId = radiologyItemId;
            Qty = qty;
        }

        public RadiologyExaminationPreferenceId ExaminationPreferenceId { get; private set; } = default!;
        public RadiologyProductId RadiologyItemId { get; private set; } = default!;
        public decimal Qty { get; private set; } = default!;

        public void Update(decimal qty) => Qty = qty;
    }
}
