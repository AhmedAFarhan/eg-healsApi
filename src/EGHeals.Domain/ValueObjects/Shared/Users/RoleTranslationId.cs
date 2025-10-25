
namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record RoleTranslationId
    {
        public Guid Value { get; }

        private RoleTranslationId(Guid value) => Value = value;

        public static RoleTranslationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RoleTranslationId can not be empty");
            }

            return new RoleTranslationId(value);
        }
    }
}
