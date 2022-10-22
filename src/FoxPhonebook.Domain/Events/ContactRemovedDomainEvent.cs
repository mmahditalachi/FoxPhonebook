using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Domain.Events
{
    public class ContactRemovedDomainEvent : DomainEvent
    {
        public ContactRemovedDomainEvent(Contact contact)
        {
            Contact = contact;
        }

        public Contact Contact { get; }
    }
}
