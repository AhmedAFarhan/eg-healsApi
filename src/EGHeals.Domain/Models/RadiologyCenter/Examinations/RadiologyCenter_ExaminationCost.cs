using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyCenter_ExaminationCost : AuditableEntity<RadiologyCenter_ExaminationCostId>
    {
        public RadiologyCenter_ExaminationId ExaminationId { get; set; } = default!;
        public decimal Cost { get; private set; }

        public RadiologyCenter_Examination Examination { get; private set; } = default!; /* NAVIGATION PROPERTY */

        public static RadiologyCenter_ExaminationCost Create(RadiologyCenter_ExaminationCostId id, RadiologyCenter_ExaminationId examinationId, decimal cost)
        {
            //Domain validation
            if (cost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cost), cost, "Examination cost should not be less than zero.");
            }

            var radiologyExaminationCost = new RadiologyCenter_ExaminationCost
            {
                Id = id,
                ExaminationId = examinationId,
                Cost = cost
            };

            return radiologyExaminationCost;
        }

        public void Update(decimal cost)
        {
            //Domain validation
            if (cost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cost), cost, "Examination cost should not be less than zero.");
            }

            Cost = cost;
        }
    }
}
