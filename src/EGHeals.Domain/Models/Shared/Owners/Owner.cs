using EGHeals.Domain.ValueObjects.Shared.Owners;
using System.Reflection;
using System.Xml.Linq;

namespace EGHeals.Domain.Models.Shared.Owners
{
    public class Owner : Entity<OwnerId>
    {
        public string NationalId { get; private set; } = default!;
        public Gender Gender { get; private set; } = Gender.MALE;
        public UserActivity UserActivity { get; private set; } = UserActivity.RADIOLOGY;
        public string ActivityDescription { get; private set; } = default!;

        public static Owner Create(string nationalId, Gender gender, UserActivity userActivity,  string activityDescription)
        {
            //Domain model validation
            if (!Enum.IsDefined(userActivity))
            {
                throw new ArgumentException("user activity value is out of range.", nameof(userActivity));
            }

            Validation(nationalId, gender, activityDescription);

            var owner = new Owner
            {
                Id = OwnerId.Of(Guid.NewGuid()),
                NationalId = nationalId,
                UserActivity = userActivity,
                Gender = gender,
                ActivityDescription = activityDescription,
            };

            return owner;
        }
        public void Update(string nationalId, Gender gender, string activityDescription)
        {
            //Domain model validation
            Validation(nationalId, gender, activityDescription);

            ActivityDescription = activityDescription;
            NationalId = nationalId;
            Gender = gender;
        }

        private static void Validation(string nationalId, Gender gender, string activityDescription)
        {
            if (string.IsNullOrEmpty(nationalId) || string.IsNullOrWhiteSpace(nationalId))
            {
                throw new ArgumentException("nationalId cannot be null, empty, or whitespace.", nameof(nationalId));
            }
            if (nationalId.Length != 14)
            {
                throw new ArgumentOutOfRangeException(nameof(nationalId), nationalId.Length, "nationalId should be in range 14 characters.");
            }
            if (!nationalId.All(char.IsDigit))
            {
                throw new ArgumentException("nationalId must contain digits only.", nameof(nationalId));
            }

            if (!Enum.IsDefined(gender))
            {
                throw new ArgumentException("gender value is out of range.", nameof(gender));
            }

            if (string.IsNullOrEmpty(activityDescription) || string.IsNullOrWhiteSpace(activityDescription))
            {
                throw new ArgumentException("activity description cannot be null, empty, or whitespace.", nameof(activityDescription));
            }

            if (activityDescription.Length < 3 || activityDescription.Length > 250)
            {
                throw new ArgumentOutOfRangeException(nameof(activityDescription), activityDescription.Length, "activity description should be in range between 3 and 150 characters.");
            }
        }
    }
}
