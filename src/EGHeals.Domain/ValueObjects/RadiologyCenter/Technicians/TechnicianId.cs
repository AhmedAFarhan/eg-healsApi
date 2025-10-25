namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record TechnicianId
    {
        public Guid Value { get; }

        private TechnicianId(Guid value) => Value = value;

        public static TechnicianId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TechnicianId can not be empty");
            }

            return new TechnicianId(value);
        }
    }
}
