using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;
using EGHeals.Domain.ValueObjects.Shared.Owners;

namespace EGHeals.Domain.Models.RadiologyCenter.Products
{
    public class RadiologyStock : Entity<RadiologyStockId>
    {
        public RadiologyProductId RadiologyItemId { get; private set; } = default!;
        public StoreId StoreId { get; private set; } = default!;
        public decimal Qty { get; private set; }
        public decimal CriticalQty { get; private set; }

        public static RadiologyStock Create(RadiologyProductId radiologyItemId, StoreId storeId, decimal qty, decimal criticalQty)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(criticalQty, 0);

            var stock = new RadiologyStock
            {
                Id = RadiologyStockId.Of(Guid.NewGuid()),
                RadiologyItemId = radiologyItemId,
                StoreId = storeId,
                Qty = qty,
                CriticalQty = criticalQty,
            };

            return stock;
        }
        public void Update(decimal qty, decimal criticalQty)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(criticalQty, 0);

            Qty = qty;
            CriticalQty = criticalQty;
        }
    }
}
