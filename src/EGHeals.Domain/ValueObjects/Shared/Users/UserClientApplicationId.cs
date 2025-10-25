namespace EGHeals.Domain.ValueObjects.Shared.Users
{
    public record UserClientApplicationId
    {
        public Guid Value { get; }

        private UserClientApplicationId(Guid value) => Value = value;

        public static UserClientApplicationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("UserClientApplicationId can not be empty");
            }

            return new UserClientApplicationId(value);
        }
    }
}
