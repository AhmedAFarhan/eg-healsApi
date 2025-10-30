using BuildingBlocks.Domain.Abstractions.Interfaces;
using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions
{
    public abstract class Entity<T> : SystemEntity<T>, IEntity<T>
    {
        public UserId OwnershipId { get; set; } = default!;
    }
}
