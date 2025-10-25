namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IAggregate<T> : IAggregate, IEntity<T> { }

    public interface IAggregate : IBaseAggregate, IEntity { }
}
