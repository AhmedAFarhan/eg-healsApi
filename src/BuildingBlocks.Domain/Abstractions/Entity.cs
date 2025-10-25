using BuildingBlocks.Domain.Abstractions.Interfaces;
using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions
{
    public abstract class Entity<T> : SystemEntity<T>, IEntity<T>
    {
        public SystemUserId OwnershipId { get; set; } = default!;
    }
}
