using EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Encounters
{
    public class RadiologyCenter_EncounterConsumable : AuditableEntity<RadiologyCenter_EncounterConsumableId>
    {
        internal RadiologyCenter_EncounterConsumable(RadiologyCenter_EncounterId encounterId, RadiologyCenter_ProductId radiologyItemId, decimal qty)
        {
            Id = RadiologyCenter_EncounterConsumableId.Of(Guid.NewGuid());
            EncounterId = encounterId;
            RadiologyItemId = radiologyItemId;
            Qty = qty;
        }

        public RadiologyCenter_EncounterId EncounterId { get; private set; } = default!;
        public RadiologyCenter_ProductId RadiologyItemId { get; private set; } = default!;
        public decimal Qty { get; private set; } = default!;

        public void Update(decimal qty) => Qty = qty;
    }
}
