namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record RadiologyCenter_TechnicianDeviceId
    {
        public Guid Value { get; }

        private RadiologyCenter_TechnicianDeviceId(Guid value) => Value = value;

        public static RadiologyCenter_TechnicianDeviceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("TechnicianDeviceId can not be empty");
            }

            return new RadiologyCenter_TechnicianDeviceId(value);
        }
    }
}
