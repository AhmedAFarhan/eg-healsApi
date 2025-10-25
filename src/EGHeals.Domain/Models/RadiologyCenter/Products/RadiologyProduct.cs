using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Products
{
    public class RadiologyProduct : SystemEntity<RadiologyProductId>
    {
        public string Name { get; private set; } = default!;
        public RadiologyCategory RadiologyCategory { get; private set; } = default!;
        public decimal UnitPrice { get; private set; }

        public static RadiologyProduct Create(string name, RadiologyCategory radiologyCategory, decimal unitPrice)
        {
            Validation(name, radiologyCategory, unitPrice);

            var item = new RadiologyProduct
            {
                Id = RadiologyProductId.Of(Guid.NewGuid()),
                Name = name,
                RadiologyCategory = radiologyCategory,
                UnitPrice = unitPrice,
            };

            return item;
        }
        public void Update(string name, RadiologyCategory radiologyCategory, decimal unitPrice)
        {
            Validation(name, radiologyCategory, unitPrice);

            Name = name;
            RadiologyCategory = radiologyCategory;
            UnitPrice = unitPrice;
        }

        private static void Validation(string name, RadiologyCategory radiologyCategory, decimal unitPrice)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            if (!Enum.IsDefined(radiologyCategory))
            {
                throw new DomainException("radiologyCategory value is out of range");
            }

            ArgumentOutOfRangeException.ThrowIfLessThan(unitPrice, 0);
        }
    }
}
