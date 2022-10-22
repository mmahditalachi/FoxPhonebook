using FoxPhonebook.Application.Common.Models;
using FoxPhonebook.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FoxPhonebook.Application.Contacts.EventHandlers
{
    public class ContactRemovedDomainEventHandler : INotificationHandler<DomainEventNotification<ContactRemovedDomainEvent>>
    {
        private readonly ILogger<ContactRemovedDomainEventHandler> _logger;

        public ContactRemovedDomainEventHandler(ILogger<ContactRemovedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ContactRemovedDomainEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("FoxPhonebook Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
