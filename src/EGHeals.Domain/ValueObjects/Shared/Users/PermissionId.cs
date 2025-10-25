namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record PermissionId
    {
        public Guid Value { get; }

        private PermissionId(Guid value) => Value = value;

        public static PermissionId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("PermissionId can not be empty");
            }

            return new PermissionId(value);
        }
    }
}
