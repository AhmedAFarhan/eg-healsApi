namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyCenter_ExaminationPreferenceId
    {
        public Guid Value { get; }

        private RadiologyCenter_ExaminationPreferenceId(Guid value) => Value = value;

        public static RadiologyCenter_ExaminationPreferenceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationPreferenceId can not be empty");
            }

            return new RadiologyCenter_ExaminationPreferenceId(value);
        }
    }
}
