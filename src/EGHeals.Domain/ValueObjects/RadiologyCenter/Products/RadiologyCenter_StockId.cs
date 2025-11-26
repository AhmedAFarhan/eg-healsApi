namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Products
{
    public record RadiologyCenter_StockId
    {
        public Guid Value { get; }

        private RadiologyCenter_StockId(Guid value) => Value = value;

        public static RadiologyCenter_StockId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("StockId can not be empty");
            }

            return new RadiologyCenter_StockId(value);
        }
    }
}
