namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologistExaminationCostId
    {
        public Guid Value { get; }

        private RadiologistExaminationCostId(Guid value) => Value = value;

        public static RadiologistExaminationCostId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologistExaminationCostId can not be empty");
            }

            return new RadiologistExaminationCostId(value);
        }
    }
}
