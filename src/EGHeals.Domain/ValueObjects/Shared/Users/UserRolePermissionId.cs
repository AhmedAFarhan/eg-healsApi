namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record UserRolePermissionId
    {
        public Guid Value { get; }

        private UserRolePermissionId(Guid value) => Value = value;

        public static UserRolePermissionId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("UserRolePermissionId can not be empty");
            }

            return new UserRolePermissionId(value);
        }
    }
}
