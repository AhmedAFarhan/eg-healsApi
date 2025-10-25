namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters
{
    public record RadiologyEncounterId
    {
        public Guid Value { get; }

        private RadiologyEncounterId(Guid value) => Value = value;

        public static RadiologyEncounterId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("EncounterId can not be empty");
            }

            return new RadiologyEncounterId(value);
        }
    }
}
