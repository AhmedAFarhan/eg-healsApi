using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IAuditableEntity<T> : IAuditableEntity, IBaseAuditableEntity<T> { }
    public interface IAuditableEntity : IBaseAuditableEntity
    {
        TenantId TenantId { get; set; }
    }
}
