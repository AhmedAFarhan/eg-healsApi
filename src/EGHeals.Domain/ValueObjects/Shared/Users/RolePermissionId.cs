namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record RolePermissionId
    {
        public Guid Value { get; }

        private RolePermissionId(Guid value) => Value = value;

        public static RolePermissionId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RolePermissionId can not be empty");
            }

            return new RolePermissionId(value);
        }
    }
}
