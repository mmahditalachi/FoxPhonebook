using FoxPhonebook.Application.Common.Exceptions;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using FoxPhonebook.Domain.Events;

namespace FoxPhonebook.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<Guid>
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string CompanyName { get; init; } = string.Empty;
        public DateTime BirthDate { get; init; }
        public bool IsFavorite { get; init; }

        public IReadOnlyCollection<Guid> TagIdList { get; init; } = new List<Guid>();
        public IReadOnlyCollection<ContactEmail> EmailList { get; init; } = new List<ContactEmail>();
        public IReadOnlyCollection<ContactPhoneNumber> PhoneNumberList { get; init; } = new List<ContactPhoneNumber>();
    }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public UpdateContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts
                .Include(e => e.ContactTags)
                .SingleOrDefaultAsync(e => e.Id == request.Id) ?? throw new NotFoundException(nameof(Contact), request.Id);

            var personalDetails = new ContactPersonalDetails(request.FirstName, request.LastName, request.CompanyName);

            contact.Update(personalDetails, DateOnly.FromDateTime(request.BirthDate), request.IsFavorite);

            var removedEmails = contact.Emails.Except(request.EmailList).ToList();
            foreach (var contactEmail in removedEmails)
                contact.RemoveContactEmail(contactEmail.Email);
            foreach (var contactEmail in request.EmailList)
                contact.AddOrUpdateContactEmail(contactEmail);

            var removedPhoneNumbers = contact.PhoneNumbers.Except(request.PhoneNumberList).ToList();
            foreach (var contactPhoneNumber in removedPhoneNumbers)
                contact.RemovePhoneNumber(contactPhoneNumber.PhoneNumber);
            foreach (var contactPhoneNumber in request.PhoneNumberList)
                contact.AddOrUpdatePhoneNumber(contactPhoneNumber);

            var removedTags = contact.ContactTags.Select(e => e.TagId).Except(request.TagIdList).ToList();
            foreach (var tagId in removedTags)
                contact.RemoveContactTag(tagId);
            foreach (var tagId in request.TagIdList)
            {
                var tag = await _context.Tags.SingleOrDefaultAsync(e => e.Id == tagId)
                    ?? throw new NotFoundException(nameof(Tag), tagId);

                contact.AddContactTag(tag);
            }

            contact.DomainEvents.Add(new ContactUpdatedDomainEvent(contact));

            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();

            return contact.Id;
        }
    }
}
