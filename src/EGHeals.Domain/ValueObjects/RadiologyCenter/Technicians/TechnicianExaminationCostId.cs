namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record TechnicianExaminationCostId
    {
        public Guid Value { get; }

        private TechnicianExaminationCostId(Guid value) => Value = value;

        public static TechnicianExaminationCostId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TechnicianExaminationCostId can not be empty");
            }

            return new TechnicianExaminationCostId(value);
        }
    }
}
