namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyExaminationId
    {
        public Guid Value { get; }

        private RadiologyExaminationId(Guid value) => Value = value;

        public static RadiologyExaminationId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationId can not be empty");
            }

            return new RadiologyExaminationId(value);
        }
    }
}
