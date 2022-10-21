using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel
{
    public class Contact : AggregateRoot<Guid>
    {
        protected Contact()
        {

        }

        public Contact(ContactPersonalDetails personalDetails, DateOnly birthDate, bool isFavorite)
        {
            PersonalDetails = personalDetails;
            BirthDate = birthDate;
            IsFavorite = isFavorite;

            _phoneNumbers = new List<ContactPhoneNumber>();
            _emails = new List<ContactEmail>();
            _contactTags = new List<ContactTag>();
        }

        public ContactPersonalDetails PersonalDetails { get; private set; }

        public DateOnly BirthDate { get; private set; }
        public bool IsFavorite { get; private set; }

        private List<ContactPhoneNumber> _phoneNumbers;
        public IReadOnlyCollection<ContactPhoneNumber> PhoneNumbers => _phoneNumbers;

        private List<ContactEmail> _emails;
        public IReadOnlyCollection<ContactEmail> Emails => _emails;

        private List<ContactTag> _contactTags;
        public IReadOnlyCollection<ContactTag> ContactTags => _contactTags;

        //public void AddContactTag(ContactTag contactTag)
        //{
        //    if (contactTag is null)
        //        throw new ArgumentNullException();

        //    if (_contactTag.Any(e => e.Id == contactTag.Id))
        //        return;

        //    _contactTag.Add(contactTag);
        //}

        //public void AddContactEmail(ContactEmail contactEmail)
        //{
        //    if (contactEmail is null)
        //        throw new ArgumentNullException();

        //    if (_contactEmails.Any(e => e.Email == contactEmail.Email))
        //        return;

        //    _contactTag.Add(contactTag);
        //}
    }
}
