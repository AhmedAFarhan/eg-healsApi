namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyExaminationPreferenceId
    {
        public Guid Value { get; }

        private RadiologyExaminationPreferenceId(Guid value) => Value = value;

        public static RadiologyExaminationPreferenceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationPreferenceId can not be empty");
            }

            return new RadiologyExaminationPreferenceId(value);
        }
    }
}
