using FoxPhonebook.Application.Common.Exceptions;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using FoxPhonebook.Domain.Events;

namespace FoxPhonebook.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<Guid>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string CompanyName { get; init; } = string.Empty;
        public DateTime BirthDate { get; init; }
        public bool IsFavorite { get; init; }

        public IReadOnlyCollection<Guid> TagIdList { get; init; } = new List<Guid>();
        public IReadOnlyCollection<ContactEmail> EmailList { get; init; } = new List<ContactEmail>();
        public IReadOnlyCollection<ContactPhoneNumber> PhoneNumberList { get; init; } = new List<ContactPhoneNumber>();

    }

    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var personalDetails = new ContactPersonalDetails(request.FirstName, request.LastName, request.CompanyName);
            var contact = new Contact(personalDetails, DateOnly.FromDateTime(request.BirthDate), request.IsFavorite);

            foreach (var email in request.EmailList)
                contact.AddOrUpdateContactEmail(email);

            foreach (var phoneNumber in request.PhoneNumberList)
                contact.AddOrUpdatePhoneNumber(phoneNumber);

            foreach (var tagId in request.TagIdList)
            {
                var tag = await _context.Tags.SingleOrDefaultAsync(e => e.Id == tagId)
                    ?? throw new NotFoundException(nameof(Tag), tagId);

                contact.AddContactTag(tag);
            }

            contact.DomainEvents.Add(new ContactCreatedDomainEvent(contact));

            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync();

            return contact.Id;
        }
    }
}
