namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologyCenter_RadiologistDeviceId
    {
        public Guid Value { get; }

        private RadiologyCenter_RadiologistDeviceId(Guid value) => Value = value;

        public static RadiologyCenter_RadiologistDeviceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologistDeviceId can not be empty");
            }

            return new RadiologyCenter_RadiologistDeviceId(value);
        }
    }
}
