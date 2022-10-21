using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoxPhonebook.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; }
        DbSet<Tag> Tags { get; }

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
