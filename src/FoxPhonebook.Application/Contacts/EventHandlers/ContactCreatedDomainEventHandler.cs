using FoxPhonebook.Application.Common.Models;
using FoxPhonebook.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FoxPhonebook.Application.Contacts.EventHandlers
{
    public class ContactCreatedDomainEventHandler : INotificationHandler<DomainEventNotification<ContactCreatedDomainEvent>>
    {
        private readonly ILogger<ContactCreatedDomainEventHandler> _logger;

        public ContactCreatedDomainEventHandler(ILogger<ContactCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ContactCreatedDomainEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("FoxPhonebook Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
