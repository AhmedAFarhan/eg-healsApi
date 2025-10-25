namespace EGHeals.Domain.ValueObjects.Shared.ReferralDoctors
{
    public record ReferralDoctorCost
    {
        public decimal Value { get; }
        private ReferralDoctorCost(decimal value) => Value = value;

        public static ReferralDoctorCost Of(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            return new ReferralDoctorCost(value);
        }
    }
}
