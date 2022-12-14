using FoxPhonebook.Application.Common.Mappings;
using FoxPhonebook.Application.Contacts.Queries.GetContact;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Application.Contacts.Queries.GetContactList
{
    public class GetContactListDto : IMapFrom<Contact>
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string CompanyName { get; init; } = string.Empty;

        public DateTime BirthDate { get; init; }
        public bool IsFavorite { get; init; }

        public IEnumerable<GetContactTagDto> TagList { get; init; } = new List<GetContactTagDto>();
        public IEnumerable<ContactEmail> EmailList { get; init; } = new List<ContactEmail>();
        public IEnumerable<ContactPhoneNumber> PhoneNumberList { get; init; } = new List<ContactPhoneNumber>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, GetContactListDto>()
                .ForMember(e => e.BirthDate, d => d.MapFrom(sm => new DateTime(sm.BirthDate.Year, sm.BirthDate.Month, sm.BirthDate.Day)))
                .ForMember(e => e.FirstName, d => d.MapFrom(sm => sm.PersonalDetails.FirstName))
                .ForMember(e => e.LastName, d => d.MapFrom(sm => sm.PersonalDetails.LastName))
                .ForMember(e => e.CompanyName, d => d.MapFrom(sm => sm.PersonalDetails.CompanyName))
                .ForMember(e => e.EmailList, d => d.MapFrom(sm => sm.Emails))
                .ForMember(e => e.PhoneNumberList, d => d.MapFrom(sm => sm.PhoneNumbers))
                .ForMember(e => e.TagList, d => d.MapFrom(sm => sm.ContactTags.Select(e => new GetContactTagDto { Id = e.Tag.Id, Title = e.Tag.Title })));
        }
    }
}
