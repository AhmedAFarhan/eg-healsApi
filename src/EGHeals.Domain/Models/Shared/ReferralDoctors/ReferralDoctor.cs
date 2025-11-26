using EGHeals.Domain.ValueObjects.Shared.ReferralDoctors;

namespace EGHeals.Domain.Models.Shared.ReferralDoctors
{
    public class ReferralDoctor : AuditableEntity<ReferralDoctorId>
    {
        public string FullName { get; private set; } = default!;
        public string? NationalId { get; private set; } = default!;
        public string Mobile { get; private set; } = default!;

        public static ReferralDoctor Create(string fullName, string? nationalId, string mobile)
        {
            Validation(fullName, nationalId, mobile);

            var referralDoctor = new ReferralDoctor
            {
                Id = ReferralDoctorId.Of(Guid.NewGuid()),
                FullName = fullName,
                NationalId = nationalId,
                Mobile = mobile,
            };

            return referralDoctor;
        }
        public void Update(string fullName, string? nationalId, string mobile)
        {
            Validation(fullName, nationalId, mobile);

            FullName = fullName;
            NationalId = nationalId;
            Mobile = mobile;
        }

        private static void Validation(string fullName, string? nationalId, string mobile)
        {
            ArgumentException.ThrowIfNullOrEmpty(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(fullName.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(fullName.Length, 3);

            if (nationalId is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(nationalId);
                ArgumentOutOfRangeException.ThrowIfNotEqual(nationalId.Length, 14);
            }

            ArgumentException.ThrowIfNullOrEmpty(mobile);
            ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
            ArgumentOutOfRangeException.ThrowIfNotEqual(mobile.Length, 11);
        }
    }
}
