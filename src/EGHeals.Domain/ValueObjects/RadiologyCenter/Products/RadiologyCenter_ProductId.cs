namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Products
{
    public record RadiologyCenter_ProductId
    {
        public Guid Value { get; }

        private RadiologyCenter_ProductId(Guid value) => Value = value;

        public static RadiologyCenter_ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyItemId can not be empty");
            }

            return new RadiologyCenter_ProductId(value);
        }
    }
}
