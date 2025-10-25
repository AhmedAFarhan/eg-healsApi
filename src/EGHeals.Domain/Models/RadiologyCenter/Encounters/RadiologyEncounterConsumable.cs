using EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Encounters
{
    public class RadiologyEncounterConsumable : Entity<RadiologyEncounterConsumableId>
    {
        internal RadiologyEncounterConsumable(RadiologyEncounterId encounterId, RadiologyProductId radiologyItemId, decimal qty)
        {
            Id = RadiologyEncounterConsumableId.Of(Guid.NewGuid());
            EncounterId = encounterId;
            RadiologyItemId = radiologyItemId;
            Qty = qty;
        }

        public RadiologyEncounterId EncounterId { get; private set; } = default!;
        public RadiologyProductId RadiologyItemId { get; private set; } = default!;
        public decimal Qty { get; private set; } = default!;

        public void Update(decimal qty) => Qty = qty;
    }
}
