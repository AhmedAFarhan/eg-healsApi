namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record PermissionTranslationId
    {
        public Guid Value { get; }

        private PermissionTranslationId(Guid value) => Value = value;

        public static PermissionTranslationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("PermissionTranslationId can not be empty");
            }

            return new PermissionTranslationId(value);
        }
    }
}
