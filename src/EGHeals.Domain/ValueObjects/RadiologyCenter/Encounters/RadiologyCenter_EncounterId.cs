namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters
{
    public record RadiologyCenter_EncounterId
    {
        public Guid Value { get; }

        private RadiologyCenter_EncounterId(Guid value) => Value = value;

        public static RadiologyCenter_EncounterId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("EncounterId can not be empty");
            }

            return new RadiologyCenter_EncounterId(value);
        }
    }
}
