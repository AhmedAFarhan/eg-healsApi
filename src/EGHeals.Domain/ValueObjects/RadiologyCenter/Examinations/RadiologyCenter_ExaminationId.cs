namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyCenter_ExaminationId
    {
        public Guid Value { get; }

        private RadiologyCenter_ExaminationId(Guid value) => Value = value;

        public static RadiologyCenter_ExaminationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationId can not be empty");
            }

            return new RadiologyCenter_ExaminationId(value);
        }
    }
}
