using FoxPhonebook.Application.Common.Exceptions;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Application.Contacts.Queries.GetContact
{
    public record GetContactQuery(Guid ContactId) : IRequest<GetContactDto>;

    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, GetContactDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContactQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetContactDto> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            return await _context.Contacts
                .ProjectTo<GetContactDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == request.ContactId, cancellationToken) ?? throw new NotFoundException(nameof(Contact), request.ContactId);
        }
    }
}
