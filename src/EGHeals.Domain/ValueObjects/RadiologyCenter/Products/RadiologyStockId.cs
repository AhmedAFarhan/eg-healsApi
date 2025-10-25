namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Products
{
    public record RadiologyStockId
    {
        public Guid Value { get; }

        private RadiologyStockId(Guid value) => Value = value;

        public static RadiologyStockId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("StockId can not be empty");
            }

            return new RadiologyStockId(value);
        }
    }
}
