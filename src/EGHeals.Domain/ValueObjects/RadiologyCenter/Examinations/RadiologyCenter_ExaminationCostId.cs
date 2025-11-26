namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations
{
    public record RadiologyCenter_ExaminationCostId
    {
        public Guid Value { get; }

        private RadiologyCenter_ExaminationCostId(Guid value) => Value = value;

        public static RadiologyCenter_ExaminationCostId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologyExaminationCostId can not be empty");
            }

            return new RadiologyCenter_ExaminationCostId(value);
        }
    }
}
