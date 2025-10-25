namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record TechnicianDeviceId
    {
        public Guid Value { get; }

        private TechnicianDeviceId(Guid value) => Value = value;

        public static TechnicianDeviceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TechnicianDeviceId can not be empty");
            }

            return new TechnicianDeviceId(value);
        }
    }
}
