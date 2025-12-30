using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EGHeals.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor(Func<IUserContextService> getUserContext) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await UpdateEntities(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task UpdateEntities(DbContext context)
        {
            if (context is ApplicationIdentityDbContext db && db.IsSeeding) return;

            var userContext = getUserContext();
            var userId = userContext.UserId;
            var tenantId = userContext.TenantId;

            foreach (var entity in context.ChangeTracker.Entries<IBaseAuditableEntity>())
            {
                if (entity is IAuditableEntity auditableEntity)
                {
                    auditableEntity.TenantId = TenantId.Of(tenantId);
                }

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedBy = userId;
                    entity.Entity.CreatedAt = DateTime.Now;
                }

                if ((entity.State == EntityState.Added || entity.State == EntityState.Modified) && !entity.Entity.IsDeleted)
                {
                    entity.Entity.LastModifiedBy = userId;
                    entity.Entity.LastModifiedAt = DateTime.Now;
                }
            }
        }
    }
}
