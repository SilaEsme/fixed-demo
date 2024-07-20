﻿using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace FixedDemo.Persistence
{
    public partial class AppDbContext : DbContext, IDbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        #region DbSets
        public DbSet<Domain.Entities.User> Users { get; set; }
        public DbSet<Domain.Entities.Asset> Assets { get; set; }
        #endregion

        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : BaseObject
            => base.Set<TEntity>();
        public async Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
            where TEntity : BaseObject
            => id == Guid.Empty ?
                null :
                await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id && e.IsActive && !e.IsDeleted, cancellationToken);

        public TEntity Insert<TEntity>(TEntity entity)
            where TEntity : BaseObject
        {
            if (entity.Id == Guid.Empty) entity.Id = new Guid();
            return Set<TEntity>().Add(entity).Entity;
        }

        public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : BaseObject
            => Set<TEntity>().AddRange(entities);

        public new void Remove<TEntity>(TEntity entity)
            where TEntity : BaseObject
            => Set<TEntity>().Remove(entity);

        public void Remove<TEntity>(Guid id)
             where TEntity : BaseObject
            => Set<TEntity>().Remove(Set<TEntity>().Single(x => x.Id == id));

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
             where TEntity : BaseObject
            => Set<TEntity>().RemoveRange(entities);
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.Now;
            UpdateAuditableEntities(now);
            UpdateSoftDeletableEntities(now);
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities(DateTime now)
        {
            foreach (EntityEntry<BaseObject> entityEntry in ChangeTracker.Entries<BaseObject>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(BaseObject.CreatedAt)).CurrentValue = now;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(BaseObject.UpdatedAt)).CurrentValue = now;
                }
            }
        }

        private void UpdateSoftDeletableEntities(DateTime now)
        {
            foreach (EntityEntry<BaseObject> entityEntry in ChangeTracker.Entries<BaseObject>())
            {
                if (entityEntry.State != EntityState.Deleted)
                {
                    continue;
                }

                entityEntry.Property(nameof(BaseObject.IsDeleted)).CurrentValue = true;

                entityEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
            }
        }
        private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
        {
            if (!entityEntry.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned()))
            {
                return;
            }

            foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
            {
                referenceEntry.TargetEntry.State = EntityState.Unchanged;

                UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
            }
        }
    }
}
