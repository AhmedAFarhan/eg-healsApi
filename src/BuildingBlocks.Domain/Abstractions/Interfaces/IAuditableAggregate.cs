namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IAuditableAggregate<T> : IAuditableAggregate, IAuditableEntity<T> { }

    public interface IAuditableAggregate : IBaseAggregate, IAuditableEntity { }
}
