using BuildingBlocks.Domain.Abstractions.Interfaces;

namespace BuildingBlocks.Domain.Abstractions
{
    public class BaseAuditableEntity<T> : IBaseAuditableEntity<T>
    {
        public T Id { get; set; } = default!;
        public Guid CreatedBy { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; } = default!;
        public DateTimeOffset? LastModifiedAt { get; set; } = default!;
        public Guid? LastModifiedBy { get; set; } = default!;
        public Guid? DeletedBy { get; set; } = default!;
        public DateTimeOffset? DeletedAt { get; set; } = default!;
        public bool IsDeleted { get; set; } = default!;

        public void Delete(Guid deletedBy, DateTimeOffset deletedAt)
        {
            IsDeleted = true;
            DeletedBy = deletedBy;
            DeletedAt = deletedAt;
        }
    }
}
