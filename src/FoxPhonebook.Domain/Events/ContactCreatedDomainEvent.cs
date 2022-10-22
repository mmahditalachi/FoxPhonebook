using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Domain.Events
{
    public class ContactCreatedDomainEvent : DomainEvent
    {
        public ContactCreatedDomainEvent(Contact contact)
        {
            Contact = contact;
        }

        public Contact Contact { get; }
    }
}
