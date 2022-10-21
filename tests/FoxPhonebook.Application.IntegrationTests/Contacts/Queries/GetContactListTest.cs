using FoxPhonebook.Application.Contacts.Queries.GetContactList;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Application.IntegrationTests.Contacts.Queries
{
    using static Testing;
    public class GetContactListTest : TestBase
    {
        [Test]
        public async Task ShouldGetContactList_WhenContactExist()
        {
            // Arrange
            var tag_one = new Tag("school");
            var contact_one = new Contact(new ContactPersonalDetails("ali", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact_one.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_one.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            contact_one.AddContactTag(tag_one);
            await AddAsync(contact_one);

            var tag_two = new Tag("DFL");
            var contact_two = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact_two.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_two.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            contact_two.AddContactTag(tag_two);
            await AddAsync(contact_two);

            var query = new GetContactListQuery();

            // Act
            var result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.Items.Select(e => e.Id).Should().ContainInOrder(contact_one.Id, contact_two.Id);
        }

        [Test]
        public async Task ShouldGetContactList_WhenTagFilterApplied()
        {
            // Arrange
            var tag_one = new Tag("school");
            var contact_one = new Contact(new ContactPersonalDetails("ali", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact_one.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_one.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            contact_one.AddContactTag(tag_one);
            await AddAsync(contact_one);

            var tag_two = new Tag("DFL");
            var contact_two = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact_two.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_two.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            contact_two.AddContactTag(tag_two);
            await AddAsync(contact_two);
            var query = new GetContactListQuery() { TagId = tag_one.Id};

            // Act
            var result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
        }

        [Test]
        public async Task ShouldGetContactList_WhenPhoneNumberFilterApplied()
        {
            // Arrange
            string Phone_Number = "09127000000";

            var tag_one = new Tag("school");
            var contact_one = new Contact(new ContactPersonalDetails("ali", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact_one.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_one.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09125445000"));
            contact_one.AddContactTag(tag_one);
            await AddAsync(contact_one);

            var tag_two = new Tag("DFL");
            var contact_two = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact_two.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_two.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", Phone_Number));
            contact_two.AddContactTag(tag_two);
            await AddAsync(contact_two);
            var query = new GetContactListQuery() { PhoneNumberfilter = Phone_Number };

            // Act
            var result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
        }
    }
}
