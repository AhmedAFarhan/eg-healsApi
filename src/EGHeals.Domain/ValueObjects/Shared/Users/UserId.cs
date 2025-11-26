namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record UserId
    {
        public Guid Value { get; }

        private UserId(Guid value) => Value = value;

        public static UserId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("UserId can not be empty");
            }

            return new UserId(value);
        }
    }
}
