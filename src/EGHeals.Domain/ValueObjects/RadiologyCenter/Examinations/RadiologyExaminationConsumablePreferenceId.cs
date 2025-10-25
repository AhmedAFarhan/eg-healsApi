namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyExaminationConsumablePreferenceId
    {
        public Guid Value { get; }

        private RadiologyExaminationConsumablePreferenceId(Guid value) => Value = value;

        public static RadiologyExaminationConsumablePreferenceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationConsumablePreferenceId can not be empty");
            }

            return new RadiologyExaminationConsumablePreferenceId(value);
        }
    }
}
