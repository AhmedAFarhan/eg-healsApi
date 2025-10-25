using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians;

namespace EGHeals.Domain.Models.RadiologyCenter.Technicians
{
    public class TechnicianExaminationCost : Entity<TechnicianExaminationCostId>
    {
        internal TechnicianExaminationCost(TechnicianId technicianId, RadiologyExaminationId examinationId, decimal cost)
        {
            Id = TechnicianExaminationCostId.Of(Guid.NewGuid());
            TechnicianId = technicianId;
            ExaminationId = examinationId;
            Cost = cost;
        }

        public TechnicianId TechnicianId { get; private set; } = default!;
        public RadiologyExaminationId ExaminationId { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
    }
}
