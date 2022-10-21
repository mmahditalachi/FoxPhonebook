using FoxPhonebook.Application.Contacts.Commands.RemoveContact;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Application.IntegrationTests.Contacts.Commands
{
    using static Testing;

    public class RemoveContactTest : TestBase
    {
        [Test]
        public async Task ShouldRemoveContact_WhenContactExist()
        {
            // Arrange
            var contact = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            await AddAsync(contact);

            var cmd = new RemoveContactCommand(contact.Id);

            // Act
            await SendAsync(cmd);

            // Assert
            var item = await FindAsync<Contact>(contact.Id);
            item.Should().BeNull();
        }

        [Test]
        public async Task ShouldRemoveContact_WhenContactContainsTag()
        {
            // Arrange
            var tag = new Tag("school");

            var contact = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
            contact.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
            contact.AddContactTag(tag);
            await AddAsync(contact);

            var cmd = new RemoveContactCommand(contact.Id);

            // Act
            await SendAsync(cmd);

            // Assert
            var item = await FindAsync<Contact>(contact.Id);
            item.Should().BeNull();

            var contactTag = await FindAsync<ContactTag>(tag.Id, contact.Id);
            contactTag.Should().BeNull();
        }
    }
}
