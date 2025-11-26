using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;
using EGHeals.Domain.ValueObjects.Shared.Owners;

namespace EGHeals.Domain.Models.RadiologyCenter.Products
{
    public class RadiologyCenter_Stock : AuditableEntity<RadiologyCenter_StockId>
    {
        public RadiologyCenter_ProductId RadiologyItemId { get; private set; } = default!;
        public StoreId StoreId { get; private set; } = default!;
        public decimal Qty { get; private set; }
        public decimal CriticalQty { get; private set; }

        public static RadiologyCenter_Stock Create(RadiologyCenter_ProductId radiologyItemId, StoreId storeId, decimal qty, decimal criticalQty)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(criticalQty, 0);

            var stock = new RadiologyCenter_Stock
            {
                Id = RadiologyCenter_StockId.Of(Guid.NewGuid()),
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
