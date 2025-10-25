namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IDomainEvent
    {
        Guid Id => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        string? EventType => GetType().AssemblyQualifiedName;
    }
}
