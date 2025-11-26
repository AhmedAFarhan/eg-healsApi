using EGHeals.Domain.ValueObjects.Shared.Allowances;

namespace EGHeals.Domain.Models.Shared.Allowances
{
    public class Allowance : AuditableEntity<AllowanceId>
    {
        public string Name { get; private set; } = default!;
        public decimal Cost { get; private set; } = default!;
        public string? Description { get; private set; } = default!;

        public static Allowance Create(string name, decimal cost, string? description)
        {
            Validation(name, cost, description);

            var allowance = new Allowance
            {
                Id = AllowanceId.Of(Guid.NewGuid()),
                Name = name,
                Cost = cost,
                Description = description,
            };

            return allowance;
        }
        public void Update(string name, decimal cost, string? description)
        {
            Validation(name, cost, description);

            Name = name;
            Cost = cost;
            Description = description;            
        }

        private static void Validation(string name, decimal cost, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            if (description is not null)
            {
                ArgumentException.ThrowIfNullOrEmpty(description);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(description.Length, 250);
                ArgumentOutOfRangeException.ThrowIfLessThan(description.Length, 3);
            }

            ArgumentOutOfRangeException.ThrowIfNegative(cost);           
        }
    }
}
