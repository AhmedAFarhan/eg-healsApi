using EGHeals.Domain.Enums.Shared;
using EGHeals.Domain.ValueObjects.Shared.Patients;

namespace EGHeals.Domain.Models.Shared.Patients
{
    public class Patient : AuditableEntity<PatientId>
    {
        public string FullName { get; private set; } = default!;
        public string? NationalId { get; private set; } = default!;
        public string? Mobile { get; private set; } = default!;
        public DateOnly? DateOfBirth { get; private set; }
        public Gender Gender { get; private set; } = Gender.MALE;
        public GuardianId? GuardianId { get; private set; } = default!;
        public int? Age { get => DateOfBirth is null ? null : DateTime.Today.Year - DateOfBirth?.Year; }

        public static Patient Create(string fullName, string? nationalId, string? mobile, DateOnly? dateOfBirth, Gender gender, GuardianId? guardianId)
        {
            Validation(fullName, nationalId, mobile, gender);

            var patient = new Patient
            {
                Id = PatientId.Of(Guid.NewGuid()),
                FullName = fullName,
                NationalId = nationalId,
                Mobile = mobile,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                GuardianId = guardianId,
            };

            return patient;
        }
        public void Update(string fullName, string? nationalId, string? mobile, DateOnly? dateOfBirth, Gender gender)
        {
            Validation(fullName, nationalId, mobile, gender);

            FullName = fullName;
            NationalId = nationalId;
            Mobile = mobile;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        private static void Validation(string fullName, string? nationalId, string? mobile, Gender gender)
        {
            ArgumentException.ThrowIfNullOrEmpty(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(fullName.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(fullName.Length, 3);

            if(mobile is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
                ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
                ArgumentOutOfRangeException.ThrowIfNotEqual(mobile.Length, 11);
            }

            if (nationalId is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(nationalId);
                ArgumentException.ThrowIfNullOrEmpty(nationalId);
                ArgumentOutOfRangeException.ThrowIfNotEqual(nationalId.Length, 11);
            }

            if (!Enum.IsDefined(gender))
            {
                throw new DomainException("gender value is out of range");
            }
        }
    }
}
