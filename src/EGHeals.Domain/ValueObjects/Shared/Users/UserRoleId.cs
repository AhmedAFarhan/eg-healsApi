namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record UserRoleId
    {
        public Guid Value { get; }

        private UserRoleId(Guid value) => Value = value;

        public static UserRoleId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("UserRoleId can not be empty");
            }

            return new UserRoleId(value);
        }
    }
}
