using FoxPhonebook.Application.Common.Mappings;
using FoxPhonebook.Application.Common.Models;

namespace FoxPhonebook.Application.Contacts.Queries.GetContactList
{
    public record GetContactListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<GetContactListDto>>
    {
        public Guid? TagId { get; init; }
        public string? PhoneNumberfilter { get; init; }
    }

    public class GetContactListQueryHandler : IRequestHandler<GetContactListQuery, PaginatedList<GetContactListDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContactListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetContactListDto>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Contacts.AsQueryable();

            if (request.TagId.HasValue)
                query = query.Where(e => e.ContactTags.Any(p => p.TagId == request.TagId.Value));
            if (!string.IsNullOrEmpty(request.PhoneNumberfilter))
                query = query.Where(e => e.PhoneNumbers.Any(e => e.PhoneNumber.Contains(request.PhoneNumberfilter)));

            return await query.ProjectTo<GetContactListDto>(_mapper.ConfigurationProvider)
                .OrderBy(e => e.LastName)
                .OrderBy(e => e.FirstName)
                .AsNoTracking()
                .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
