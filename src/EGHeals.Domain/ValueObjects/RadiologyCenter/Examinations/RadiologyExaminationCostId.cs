namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyExaminationCostId
    {
        public Guid Value { get; }

        private RadiologyExaminationCostId(Guid value) => Value = value;

        public static RadiologyExaminationCostId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationCostId can not be empty");
            }

            return new RadiologyExaminationCostId(value);
        }
    }
}
