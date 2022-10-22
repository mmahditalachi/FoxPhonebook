using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Domain.Events
{
    public class ContactUpdatedDomainEvent : DomainEvent
    {
        public ContactUpdatedDomainEvent(Contact contact)
        {
            Contact = contact;
        }

        public Contact Contact { get; }
    }
}
