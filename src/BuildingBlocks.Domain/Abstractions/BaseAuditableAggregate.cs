using BuildingBlocks.Domain.Abstractions.Interfaces;

namespace BuildingBlocks.Domain.Abstractions
{
    public class BaseAuditableAggregate<T> : BaseAuditableEntity<T>, IBaseAuditableAggregate<T>
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
