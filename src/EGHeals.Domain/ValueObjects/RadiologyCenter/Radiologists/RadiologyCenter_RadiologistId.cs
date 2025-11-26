using EGHeals.Domain.Models;
namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologyCenter_RadiologistId
    {
        public Guid Value { get; }

        private RadiologyCenter_RadiologistId(Guid value) => Value = value;

        public static RadiologyCenter_RadiologistId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologistId can not be empty");
            }

            return new RadiologyCenter_RadiologistId(value);
        }
    }
}
