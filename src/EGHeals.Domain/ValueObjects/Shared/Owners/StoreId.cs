namespace EGHeals.Domain.ValueObjects.Shared.Owners
{
    public record StoreId
    {
        public Guid Value { get; }

        private StoreId(Guid value) => Value = value;

        public static StoreId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("StoreId can not be empty");
            }

            return new StoreId(value);
        }
    }
}
