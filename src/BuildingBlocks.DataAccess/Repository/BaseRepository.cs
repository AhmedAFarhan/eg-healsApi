using BuildingBlocks.DataAccessAbstraction.Queries;
using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Abstractions.Interfaces;
using BuildingBlocks.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace BuildingBlocks.DataAccess.Repository
{
    public class BaseRepository<T, TId, TContext>(TContext dbContext, ICurrentUserService userContext) : IBaseRepository<T, TId> where T : class, ISystemEntity<TId> where TId : class where TContext : DbContext 
    {
        protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

        /************************************** Query methods ***************************************/

        public async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options,
                                                      bool includeDeleted = false,
                                                      bool includeOwnership = false,
                                                      Expression<Func<T, object>>[]? includes = null,
                                                      CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = _dbSet.AsQueryable();

            //Apply includes
            query = ApplyIncludes(query, includes);

            //Included deleted entities
            query = IncludeDeletedEntities(query, includeDeleted);

            //Include Ownership
            query = IncludeOwnership(query, includeOwnership);

            // Apply filtering
            var filterExpression = options.QueryFilters.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            // Apply sorting
            query = options.ApplySorting(query);

            // Apply pagination
            query = query.Skip(options.Skip).Take(options.Take);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<T?> GetByIdAsync(TId id,
                                           bool includeDeleted = false,
                                           bool includeOwnership = false,
                                           Expression<Func<T, object>>[]? includes = null,
                                           CancellationToken cancellationToken = default)
        {
            //Starting query
            var query = _dbSet.AsQueryable();

            //Included deleted entities
            query = IncludeDeletedEntities(query, includeDeleted);

            //Apply Ownership
            query = IncludeOwnership(query, includeOwnership);

            //Apply Includes
            query = ApplyIncludes(query, includes);

            return await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public async Task<long> GetCountAsync(QueryFilters<T> filters,
                                              bool includeDeleted = false,
                                              bool includeOwnership = false,
                                              CancellationToken cancellationToken = default)
        {
            ///Starting query
            var query = _dbSet.AsQueryable();

            //Included deleted entities
            query = IncludeDeletedEntities(query, includeDeleted);

            //Apply Ownership
            query = IncludeOwnership(query, includeOwnership);

            // Apply filtering
            var filterExpression = filters.BuildFilterExpression();
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            return await query.LongCountAsync(cancellationToken);
        }

        /************************************** Command methods ***************************************/

        public async Task<T> AddOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public void HardDelete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity) => _dbSet.Update(entity);


        /************************************** Helper methods ***************************************/
        protected IQueryable<T> IncludeDeletedEntities(IQueryable<T> query, bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                query = query.Where(x => x.IsDeleted == includeDeleted);
            }

            return query;
        }

        protected IQueryable<T> IncludeOwnership(IQueryable<T> query, bool includeOwnership = false)
        {
            if (includeOwnership && typeof(IEntity).IsAssignableFrom(typeof(T)))
            {
                var ownedBy = userContext.OwnershipId;
                query = query.Where(x => ((IEntity)x).OwnershipId == UserId.Of(ownedBy.Value));
            }
            return query;
        }

        protected IQueryable<T> ApplyIncludes(IQueryable<T> query, Expression<Func<T, object>>[]? includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.AsSplitQuery().Include(include);
                }
            }
            return query;
        }

    }
}
