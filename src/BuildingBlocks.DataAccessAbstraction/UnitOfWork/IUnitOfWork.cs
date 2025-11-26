using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.DataAccessAbstraction.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T, TId> GetRepository<T, TId>() where T : BaseAuditableEntity<TId> where TId : class;
        TRepository GetCustomRepository<TRepository>() where TRepository : class;
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
