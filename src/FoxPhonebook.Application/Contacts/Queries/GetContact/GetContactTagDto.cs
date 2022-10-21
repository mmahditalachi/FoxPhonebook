using FoxPhonebook.Application.Common.Mappings;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Application.Contacts.Queries.GetContact
{
    public class GetContactTagDto : IMapFrom<Tag>
    {
        public Guid Id { get; init; }
        public string Title { get;  init; } = string.Empty;
    }
}
