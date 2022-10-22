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

        public void RemoveContactTag(Guid tagId)
        {
            Guard.Against.Default(tagId);

            var contacttag = _contactTags.SingleOrDefault(e => e.TagId == tagId);

            if (contacttag is null)
                return;

            _contactTags.Remove(contacttag);
        }

        public void AddOrUpdateContactEmail(ContactEmail contactEmail)
        {
            if (contactEmail is null)
                throw new ArgumentNullException();

            var entry = _emails.SingleOrDefault(e => e.Email == contactEmail.Email);

            if (entry is null)
                _emails.Add(contactEmail);

            else if (!entry.Equals(contactEmail))
            {
                _emails.Remove(entry);
                _emails.Add(contactEmail);
            }
        }

        public void RemoveContactEmail(string email)
        {
            Guard.Against.NullOrEmpty(email);
            var contactEmail = _emails.SingleOrDefault(e => e.Email == email);

            if (contactEmail is null)
                return;

            _emails.Remove(contactEmail);
        }

        public void AddOrUpdatePhoneNumber(ContactPhoneNumber contactPhoneNumber)
        {
            if (contactPhoneNumber is null)
                throw new ArgumentNullException();

            var entry = _phoneNumbers.SingleOrDefault(e => e.PhoneNumber == contactPhoneNumber.PhoneNumber);

            if (entry is null)
                _phoneNumbers.Add(contactPhoneNumber);

            else if (!entry.Equals(contactPhoneNumber))
            {
                _phoneNumbers.Remove(entry);
                _phoneNumbers.Add(contactPhoneNumber);
            }
        }

        public void RemovePhoneNumber(string phoneNumber)
        {
            Guard.Against.NullOrEmpty(phoneNumber);
            var contactPhone = _phoneNumbers.SingleOrDefault(e => e.PhoneNumber == phoneNumber);

            if (contactPhone is null)
                return;

            _phoneNumbers.Remove(contactPhone);
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
