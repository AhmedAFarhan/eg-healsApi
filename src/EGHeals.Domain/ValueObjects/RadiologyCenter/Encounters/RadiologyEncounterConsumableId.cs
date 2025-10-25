namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters
{
    public record RadiologyEncounterConsumableId
    {
        public Guid Value { get; }

        private RadiologyEncounterConsumableId(Guid value) => Value = value;

        public static RadiologyEncounterConsumableId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ConsumableId can not be empty");
            }

            return new RadiologyEncounterConsumableId(value);
        }
    }
}
