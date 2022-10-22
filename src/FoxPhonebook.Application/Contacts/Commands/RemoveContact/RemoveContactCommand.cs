using FoxPhonebook.Application.Common.Exceptions;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using FoxPhonebook.Domain.Events;

namespace FoxPhonebook.Application.Contacts.Commands.RemoveContact
{
    public record RemoveContactCommand(Guid ContactId) : IRequest;

    public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public RemoveContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FindAsync(request.ContactId)
                ?? throw new NotFoundException(nameof(Contact), request.ContactId);

            contact.DomainEvents.Add(new ContactRemovedDomainEvent(contact));

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
