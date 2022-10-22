namespace FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel
{
    public class ContactTag
    {
        public ContactTag(Contact contact, Tag tag)
        {
            Contact = Guard.Against.Null(contact);
            Tag = Guard.Against.Null(tag); ;
        }

        public ContactTag(Guid contactId, Guid tagId)
        {
            ContactId = Guard.Against.Default(contactId);
            TagId = Guard.Against.Default(tagId);
        }

        public Contact Contact { get; private set; }
        public Guid ContactId { get; private set; }

        public Tag Tag { get; private set; }
        public Guid TagId { get; private set; }
    }
}
