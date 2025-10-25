
namespace BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface ISystemAggregate<T> : ISystemAggregate, ISystemEntity<T> { }
    public interface ISystemAggregate : IBaseAggregate, ISystemEntity { }
}
