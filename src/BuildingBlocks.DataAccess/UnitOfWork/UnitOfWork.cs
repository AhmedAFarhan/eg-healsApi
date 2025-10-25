using BuildingBlocks.DataAccess.Repository;
using BuildingBlocks.DataAccessAbstraction.Repository;
using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DataAccess.UnitOfWork
{
    public class UnitOfWork<TContext>(TContext dbContext, IServiceProvider serviceProvider, ICurrentUserService? userContext = null) : IUnitOfWork where TContext : DbContext
    {
        private readonly Dictionary<Type, object> _repositories = new();

        public IBaseRepository<T, TId> GetRepository<T, TId>() where T : SystemEntity<TId> where TId : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                IBaseRepository<T, TId> repository;

                repository = new BaseRepository<T, TId, TContext>(dbContext, userContext);

                _repositories[typeof(T)] = repository;
            }

            return (IBaseRepository<T, TId>)_repositories[typeof(T)];
        }

        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            return serviceProvider.GetRequiredService<TRepository>();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
