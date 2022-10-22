using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
