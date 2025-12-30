namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IBaseAuditableEntity<T> : IBaseAuditableEntity
    {
        T Id { get; set; }
    }
    public interface IBaseAuditableEntity
    {
        Guid CreatedBy { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        Guid? LastModifiedBy { get; set; }
        DateTimeOffset? LastModifiedAt { get; set; }
        Guid? DeletedBy { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
