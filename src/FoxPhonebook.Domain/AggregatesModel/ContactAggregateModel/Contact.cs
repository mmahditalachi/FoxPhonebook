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
            Id = Guid.NewGuid();

            PersonalDetails = personalDetails;
            BirthDate = Guard.Against.Default(birthDate);
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

        public void AddContactTag(Tag tag)
        {
            if (tag is null)
                throw new ArgumentNullException();

            if (_contactTags.Any(e => e.TagId == tag.Id))
                return;

            _contactTags.Add(new ContactTag(this, tag));
        }

        public void AddContactEmail(ContactEmail contactEmail)
        {
            if (contactEmail is null)
                throw new ArgumentNullException();

            if (_emails.Any(e => e.Email == contactEmail.Email))
                return;

            _emails.Add(contactEmail);
        }

        public void AddOrUpdatePhoneNumber(ContactPhoneNumber contactPhoneNumber)
        {
            if (contactPhoneNumber is null)
                throw new ArgumentNullException();

            var phoneNumber = _phoneNumbers.SingleOrDefault(e => e.PhoneNumber == contactPhoneNumber.PhoneNumber);

            if (phoneNumber is null)
                _phoneNumbers.Add(contactPhoneNumber);

            else if (!phoneNumber.Equals(contactPhoneNumber))
            {
                _phoneNumbers.Remove(phoneNumber);
                _phoneNumbers.Add(contactPhoneNumber);
            }
        }

        public void Update(ContactPersonalDetails personalDetails, DateOnly birthDate, bool isFavorite)
        {
            BirthDate = Guard.Against.Default(birthDate);
            IsFavorite = isFavorite;

            Guard.Against.Null(personalDetails);

            if (PersonalDetails.Equals(personalDetails))
                return;

            PersonalDetails = personalDetails;
        }
    }
}
