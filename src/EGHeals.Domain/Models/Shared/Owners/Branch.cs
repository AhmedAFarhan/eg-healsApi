using EGHeals.Domain.ValueObjects.Shared.Owners;

namespace EGHeals.Domain.Models.Shared.Owners
{
    public class Branch : AuditableEntity<BranchId>
    {
        public string Name { get; private set; } = default!;
        public double Latitude { get; private set; } = default!;
        public double Longitude { get; private set; } = default!;
        public string? HowToReach { get; private set; } = default!;

        public static Branch Create(string name, double latitude, double longitude, string? howToReach)
        {
            Validation(name, latitude, longitude, howToReach);

            var branch = new Branch
            {
                Id = BranchId.Of(Guid.NewGuid()),
                Name = name,
                Latitude = latitude,
                Longitude = longitude,
                HowToReach = howToReach,
            };

            return branch;
        }
        public void Update(string name, double latitude, double longitude, string? howToReach)
        {
            Validation(name, latitude, longitude, howToReach);

            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            HowToReach = howToReach;
        }

        private static void Validation(string name, double latitude, double longitude, string? howToReach)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            if (howToReach is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(howToReach);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(howToReach.Length, 250);
                ArgumentOutOfRangeException.ThrowIfLessThan(howToReach.Length, 3);
            }           
        }
    }
}
