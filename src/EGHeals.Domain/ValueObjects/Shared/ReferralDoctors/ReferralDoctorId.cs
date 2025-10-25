namespace EGHeals.Domain.ValueObjects.Shared.ReferralDoctors
{
    public record ReferralDoctorId
    {
        public Guid Value { get; }

        private ReferralDoctorId(Guid value) => Value = value;

        public static ReferralDoctorId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("ReferralDoctorId can not be empty");
            }

            return new ReferralDoctorId(value);
        }
    }
}
