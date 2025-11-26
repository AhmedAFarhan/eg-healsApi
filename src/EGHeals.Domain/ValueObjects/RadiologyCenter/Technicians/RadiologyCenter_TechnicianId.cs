namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record RadiologyCenter_TechnicianId
    {
        public Guid Value { get; }

        private RadiologyCenter_TechnicianId(Guid value) => Value = value;

        public static RadiologyCenter_TechnicianId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TechnicianId can not be empty");
            }

            return new RadiologyCenter_TechnicianId(value);
        }
    }
}
