namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record UserPermissionId
    {
        public Guid Value { get; }

        private UserPermissionId(Guid value) => Value = value;

        public static UserPermissionId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("UserPermissionId can not be empty");
            }

            return new UserPermissionId(value);
        }
    }
}
