namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IBaseAggregate
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent domainEvent);
        IDomainEvent[] ClearDomainEvents();
    }
}
