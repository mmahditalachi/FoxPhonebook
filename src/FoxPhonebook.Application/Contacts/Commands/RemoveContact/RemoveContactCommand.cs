using FoxPhonebook.Application.Common.Exceptions;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
