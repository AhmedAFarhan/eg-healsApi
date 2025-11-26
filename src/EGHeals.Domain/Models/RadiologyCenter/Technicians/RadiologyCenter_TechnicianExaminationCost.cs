using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians;

namespace EGHeals.Domain.Models.RadiologyCenter.Technicians
{
    public class RadiologyCenter_TechnicianExaminationCost : AuditableEntity<RadiologyCenter_TechnicianExaminationCostId>
    {
        internal RadiologyCenter_TechnicianExaminationCost(RadiologyCenter_TechnicianId technicianId, RadiologyCenter_ExaminationId examinationId, decimal cost)
        {
            Id = RadiologyCenter_TechnicianExaminationCostId.Of(Guid.NewGuid());
            TechnicianId = technicianId;
            ExaminationId = examinationId;
            Cost = cost;
        }

        public RadiologyCenter_TechnicianId TechnicianId { get; private set; } = default!;
        public RadiologyCenter_ExaminationId ExaminationId { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
    }
}
