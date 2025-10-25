namespace EGHeals.Domain.ValueObjects.Shared.Allowances
{
    public record AllowanceId
    {
        public Guid Value { get; }

        private AllowanceId(Guid value) => Value = value;

        public static AllowanceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("AllowanceId can not be empty");
            }

            return new AllowanceId(value);
        }
    }

}
