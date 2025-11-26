using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists;

namespace EGHeals.Domain.Models.RadiologyCenter.Radiologists
{
    public class RadiologyCenter_RadiologistExaminationCost : AuditableEntity<RadiologyCenter_RadiologistExaminationCostId>
    {
        internal RadiologyCenter_RadiologistExaminationCost(RadiologyCenter_RadiologistId radiologistId, RadiologyCenter_ExaminationId examinationId, decimal cost)
        {
            Id = RadiologyCenter_RadiologistExaminationCostId.Of(Guid.NewGuid());
            RadiologistId = radiologistId;
            ExaminationId = examinationId;
            Cost = cost;
        }

        public RadiologyCenter_RadiologistId RadiologistId { get; private set; } = default!;
        public RadiologyCenter_ExaminationId ExaminationId { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
    }
}
