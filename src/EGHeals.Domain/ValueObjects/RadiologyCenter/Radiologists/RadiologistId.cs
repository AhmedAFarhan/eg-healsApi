using EGHeals.Domain.Models;
namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologistId
    {
        public Guid Value { get; }

        private RadiologistId(Guid value) => Value = value;

        public static RadiologistId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("RadiologistId can not be empty");
            }

            return new RadiologistId(value);
        }
    }
}
