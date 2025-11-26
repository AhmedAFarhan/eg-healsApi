using EGHeals.Domain.ValueObjects.Shared.Patients;

namespace EGHeals.Domain.Models.Shared.Patients
{
    public class Guardian : AuditableEntity<GuardianId>
    {
        public string FullName { get; private set; } = default!;
        public string Mobile { get; private set; } = default!;
        public string? NationalId { get; private set; } = default!;

        public static Guardian Create(string fullName, string mobile, string? nationalId)
        {
            Validation(fullName, mobile, nationalId);

            var guardian = new Guardian
            {
                Id = GuardianId.Of(Guid.NewGuid()),
                FullName = fullName,
                Mobile = mobile,
                NationalId = nationalId,
            };

            return guardian;
        }
        public void Update(string fullName, string mobile, string? nationalId)
        {
            Validation(fullName, mobile, nationalId);

            FullName = fullName;
            Mobile = mobile;
            NationalId = nationalId;
        }

        private static void Validation(string fullName, string mobile, string? nationalId)
        {
            ArgumentException.ThrowIfNullOrEmpty(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(fullName.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(fullName.Length, 3);

            ArgumentException.ThrowIfNullOrEmpty(mobile);
            ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
            ArgumentOutOfRangeException.ThrowIfNotEqual(mobile.Length, 11);

            if (nationalId is not null)
            {
                ArgumentException.ThrowIfNullOrEmpty(nationalId);
                ArgumentOutOfRangeException.ThrowIfNotEqual(nationalId.Length, 11);
            }
        }
    }
}
