namespace EGHeals.Domain.ValueObjects.Shared.Owners
{
    public record OwnerId
    {
        public Guid Value { get; }

        private OwnerId(Guid value) => Value = value;

        public static OwnerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("OwnerId can not be empty");
            }

            return new OwnerId(value);
        }
    }
}
