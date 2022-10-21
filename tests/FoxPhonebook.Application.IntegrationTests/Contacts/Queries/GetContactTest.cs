using FoxPhonebook.Application.Contacts.Queries.GetContact;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Application.IntegrationTests.Contacts.Queries
{
    using static Testing;
    public class GetContactTest : TestBase
    {
        [Test]
        public async Task ShouldGetContact_WhenContactExist()
        {
            // Arrange
            var tag = new Tag("school");
            var contact = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            contact.AddContactTag(tag);
            await AddAsync(contact);
            var query = new GetContactQuery(contact.Id);

            // Act
            var result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result?.FirstName.Should().Be(contact.PersonalDetails.FirstName);
            result?.LastName.Should().Be(contact.PersonalDetails.LastName);
            result?.CompanyName.Should().Be(contact.PersonalDetails.CompanyName);
            result?.BirthDate.Should().Be(contact.BirthDate);
            result?.IsFavorite.Should().Be(contact.IsFavorite);
            result?.PhoneNumberList.Should().HaveCount(contact.PhoneNumbers.Count);
            result?.PhoneNumberList.Should().BeEquivalentTo(contact.PhoneNumbers);
            result?.EmailList.Should().HaveCount(contact.Emails.Count);
            result?.EmailList.Should().BeEquivalentTo(contact.Emails);
            result?.TagList.Should().HaveCount(contact.ContactTags.Count);            
        }
    }
}
