using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IEntity<T> : IEntity, ISystemEntity<T> { }
    public interface IEntity : ISystemEntity
    {
        UserId OwnershipId { get; set; }
    }
}
