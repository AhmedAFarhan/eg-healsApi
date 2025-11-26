
namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IBaseAuditableAggregate<T> : IBaseAuditableAggregate, IBaseAuditableEntity<T> { }
    public interface IBaseAuditableAggregate : IBaseAggregate, IBaseAuditableEntity { }
}
