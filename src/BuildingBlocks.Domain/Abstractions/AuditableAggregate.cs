using BuildingBlocks.Domain.Abstractions.Interfaces;

namespace BuildingBlocks.Domain.Abstractions
{
    public abstract class AuditableAggregate<T> : AuditableEntity<T>, IAuditableAggregate<T>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public IDomainEvent[] ClearDomainEvents()
        {
            var domainEventsArray = _domainEvents.ToArray();

            _domainEvents.Clear();

            return domainEventsArray;
        }
    }
}
