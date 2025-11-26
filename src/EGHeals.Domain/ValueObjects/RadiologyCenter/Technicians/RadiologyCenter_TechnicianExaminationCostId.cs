namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record RadiologyCenter_TechnicianExaminationCostId
    {
        public Guid Value { get; }

        private RadiologyCenter_TechnicianExaminationCostId(Guid value) => Value = value;

        public static RadiologyCenter_TechnicianExaminationCostId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TechnicianExaminationCostId can not be empty");
            }

            return new RadiologyCenter_TechnicianExaminationCostId(value);
        }
    }
}
