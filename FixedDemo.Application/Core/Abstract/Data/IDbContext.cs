using FixedDemo.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace FixedDemo.Application.Core.Abstract.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : BaseObject;
        Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
            where TEntity : BaseObject;
        TEntity Insert<TEntity>(TEntity entity)
            where TEntity : BaseObject;
        void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
            where TEntity : BaseObject;
        void Remove<TEntity>(TEntity entity)
            where TEntity : BaseObject;
        void Remove<TEntity>(Guid id)
            where TEntity : BaseObject;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : BaseObject;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
