using BuildingBlocks.Domain.Abstractions.Interfaces;
using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Abstractions
{
    public abstract class AuditableEntity<T> : BaseAuditableEntity<T>, IAuditableEntity<T>
    {
        public TenantId TenantId { get; set; } = default!;
    }
}
