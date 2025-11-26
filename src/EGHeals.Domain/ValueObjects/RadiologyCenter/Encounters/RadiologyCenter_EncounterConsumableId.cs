namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters
{
    public record RadiologyCenter_EncounterConsumableId
    {
        public Guid Value { get; }

        private RadiologyCenter_EncounterConsumableId(Guid value) => Value = value;

        public static RadiologyCenter_EncounterConsumableId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ConsumableId can not be empty");
            }

            return new RadiologyCenter_EncounterConsumableId(value);
        }
    }
}
