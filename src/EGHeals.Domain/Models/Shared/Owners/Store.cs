using EGHeals.Domain.ValueObjects.Shared.Owners;

namespace EGHeals.Domain.Models.Shared.Owners
{
    public class Store : AuditableEntity<StoreId>
    {
        public string Name { get; private set; } = default!;
        public BranchId BranchId { get; private set; } = default!;
        public string? Description { get; private set; } = default!;

        public static Store Create(string name, BranchId branchId, string? description)
        {
            Validation(name, description);

            var store = new Store
            {
                Id = StoreId.Of(Guid.NewGuid()),
                Name = name,
                BranchId = branchId,
                Description = description,
            };

            return store;
        }
        public void Update(string name, BranchId branchId, string? description)
        {
            Validation(name, description);

            Name = name;
            BranchId = branchId;
            Description = description;
        }

        private static void Validation(string name, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            if (description is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(description);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(description.Length, 250);
                ArgumentOutOfRangeException.ThrowIfLessThan(description.Length, 3);
            }
        }
    }
}
