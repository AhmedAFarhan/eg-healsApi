namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Products
{
    public record RadiologyProductId
    {
        public Guid Value { get; }

        private RadiologyProductId(Guid value) => Value = value;

        public static RadiologyProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyItemId can not be empty");
            }

            return new RadiologyProductId(value);
        }
    }
}
