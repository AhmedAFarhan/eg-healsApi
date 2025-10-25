namespace EGHeals.Domain.ValueObjects.Shared.Owners
{
    public record BranchId
    {
        public Guid Value { get; }

        private BranchId(Guid value) => Value = value;

        public static BranchId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("BranchId can not be empty");
            }

            return new BranchId(value);
        }
    }
}
