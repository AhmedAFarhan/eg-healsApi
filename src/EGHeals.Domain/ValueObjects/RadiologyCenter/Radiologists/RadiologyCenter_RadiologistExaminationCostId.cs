namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologyCenter_RadiologistExaminationCostId
    {
        public Guid Value { get; }

        private RadiologyCenter_RadiologistExaminationCostId(Guid value) => Value = value;

        public static RadiologyCenter_RadiologistExaminationCostId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologistExaminationCostId can not be empty");
            }

            return new RadiologyCenter_RadiologistExaminationCostId(value);
        }
    }
}
