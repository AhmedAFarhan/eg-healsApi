using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists;

namespace EGHeals.Domain.Models.RadiologyCenter.Radiologists
{
    public class RadiologistExaminationCost : Entity<RadiologistExaminationCostId>
    {
        internal RadiologistExaminationCost(RadiologistId radiologistId, RadiologyExaminationId examinationId, decimal cost)
        {
            Id = RadiologistExaminationCostId.Of(Guid.NewGuid());
            RadiologistId = radiologistId;
            ExaminationId = examinationId;
            Cost = cost;
        }

        public RadiologistId RadiologistId { get; private set; } = default!;
        public RadiologyExaminationId ExaminationId { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
    }
}
