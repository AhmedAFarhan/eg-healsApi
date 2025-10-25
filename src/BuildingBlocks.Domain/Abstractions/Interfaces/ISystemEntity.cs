namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface ISystemEntity<T> : ISystemEntity
    {
        T Id { get; set; }
    }
    public interface ISystemEntity
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        Guid? LastModifiedBy { get; set; }
        DateTime? LastModifiedAt { get; set; }
        Guid? DeletedBy { get; set; }           
        DateTime? DeletedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
