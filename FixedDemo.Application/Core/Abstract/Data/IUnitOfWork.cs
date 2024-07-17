namespace FixedDemo.Application.Core.Abstract.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
