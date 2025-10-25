namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record ClientApplicationId
    {
        public Guid Value { get; }

        private ClientApplicationId(Guid value) => Value = value;

        public static ClientApplicationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ClientApplicationId can not be empty");
            }

            return new ClientApplicationId(value);
        }
    }
}
