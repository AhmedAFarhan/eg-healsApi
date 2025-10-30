using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects
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
                throw new DomainException("SystemUserId can not be empty");
            }

            return new UserId(value);
        }
    }
}
