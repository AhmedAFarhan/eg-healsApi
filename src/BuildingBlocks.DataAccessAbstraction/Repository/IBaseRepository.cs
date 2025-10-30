using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.Domain.Abstractions.Interfaces;
using System.Linq.Expressions;

namespace BuildingBlocks.DataAccessAbstraction.Repository
{
    public interface IBaseRepository<T, TId> where T : class, ISystemEntity<TId> where TId : class
    {
        /************************************** Query methods ***************************************/

        Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options,
                                         bool includeDeleted = false,
                                         bool includeOwnership = false,
                                         Expression<Func<T, object>>[]? includes = null,
                                         CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(TId id,
                              bool includeDeleted = false,
                              bool includeOwnership = false,
                              Expression<Func<T, object>>[]? includes = null,
                              CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(QueryFilters<T> filters,
                                 bool includeDeleted = false,
                                 bool includeOwnership = false,
                                 CancellationToken cancellationToken = default);

        /************************************** Command methods ***************************************/

        Task<T> AddOneAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void SoftDelete(T entity);
        void HardDelete(T entity);
        void Update(T entity);           
    }
}
