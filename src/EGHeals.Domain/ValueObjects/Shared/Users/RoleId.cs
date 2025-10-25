namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record RoleId
    {
        public Guid Value { get; }

        private RoleId(Guid value) => Value = value;

        public static RoleId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RoleId can not be empty");
            }

            return new RoleId(value);
        }
    }
}
