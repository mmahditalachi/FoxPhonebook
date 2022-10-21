using FoxPhonebook.Application.Common.Interfaces;
using FoxPhonebook.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService
            ) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
