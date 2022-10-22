using FoxPhonebook.Application.Common.Models;
using FoxPhonebook.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FoxPhonebook.Application.Contacts.EventHandlers
{
    public class ContactUpdatedDomainEventHandler : INotificationHandler<DomainEventNotification<ContactUpdatedDomainEvent>>
    {
        private readonly ILogger<ContactUpdatedDomainEventHandler> _logger;

        public ContactUpdatedDomainEventHandler(ILogger<ContactUpdatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ContactUpdatedDomainEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("FoxPhonebook Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
