using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoxPhonebook.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
