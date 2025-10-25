using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyExaminationCost : Entity<RadiologyExaminationCostId>
    {
        public RadiologyExaminationId RadiologyExaminationId { get; set; } = default!;
        public decimal Cost { get; private set; }

        public static RadiologyExaminationCost Create(RadiologyExaminationCostId id, RadiologyExaminationId radiologyExaminationId, decimal cost)
        {
            //Domain validation
            if (cost < 0)
            {
                throw new ArgumentException("cost cannot be less than 0", nameof(Cost));
            }

            var radiologyExaminationCost = new RadiologyExaminationCost
            {
                Id = id,
                RadiologyExaminationId = radiologyExaminationId,
                Cost = cost
            };

            return radiologyExaminationCost;
        }

        public void Update(decimal cost)
        {
            //Domain validation
            if (Cost < 0)
            {
                throw new ArgumentException("cost cannot be less than 0", nameof(Cost));
            }

            Cost = cost;
        }
    }
}
