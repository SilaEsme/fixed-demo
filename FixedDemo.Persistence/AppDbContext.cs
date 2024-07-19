using Microsoft.EntityFrameworkCore;


using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Domain.Primitives;
namespace FixedDemo.Persistence
{
    public partial class AppDbContext : DbContext, IDbContext
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

    }
}
