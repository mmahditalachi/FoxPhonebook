using FoxPhonebook.Application.Common.Mappings;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Application.Contacts.Queries.GetContact
{
    public class GetContactTagDto : IMapFrom<Tag>
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
    }
}
