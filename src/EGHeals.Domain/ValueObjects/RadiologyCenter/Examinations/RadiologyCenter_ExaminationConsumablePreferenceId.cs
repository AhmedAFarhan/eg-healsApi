namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyCenter_ExaminationConsumablePreferenceId
    {
        public Guid Value { get; }

        private RadiologyCenter_ExaminationConsumablePreferenceId(Guid value) => Value = value;

        public static RadiologyCenter_ExaminationConsumablePreferenceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationConsumablePreferenceId can not be empty");
            }

            return new RadiologyCenter_ExaminationConsumablePreferenceId(value);
        }
    }
}
