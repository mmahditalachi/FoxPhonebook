using FoxPhonebook.Application.Contacts.Commands.UpdateContact;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Application.IntegrationTests.Contacts.Commands;

using static Testing;

public class UpdateContactTest : TestBase
{
    [Test]
    public async Task ShouldUpdateContact_WhenEmailAndPhoneNumberAndPersonalDetailsChanged()
    {
        // Arrange
        var contact = new Contact(new ContactPersonalDetails("mohammad", "talachi", "ddd"), new DateOnly(1999, 11, 24), true);
        contact.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
        contact.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "09127000000"));
        await AddAsync(contact);

        var tag = new Tag("school");
        await AddAsync(tag);

        var cmd = new UpdateContactCommand()
        {
            Id = contact.Id,
            FirstName = "MohammadAli",
            LastName = "TalachiPour",
            CompanyName = "arcade",
            BirthDate = new DateTime(2000, 12, 25),
            EmailList = new List<ContactEmail> { new ContactEmail("mohammadmahditalachi@gmail.com") },
            IsFavorite = false,
            PhoneNumberList = new List<ContactPhoneNumber>
            {
                new ContactPhoneNumber("home", "22222222"),
                new ContactPhoneNumber("mobile", "09127000000"),
            },

            TagIdList = new List<Guid> { tag.Id },
        };

        // Act
        var contactId = await SendAsync(cmd);

        // Assert
        var item = await FindAsync<Contact>(contactId);
        item.Should().NotBeNull();
        item?.PersonalDetails.FirstName.Should().Be(cmd.FirstName.ToLower());
        item?.PersonalDetails.LastName.Should().Be(cmd.LastName.ToLower());
        item?.PersonalDetails.CompanyName.Should().Be(cmd.CompanyName.ToLower());
        item?.BirthDate.Should().Be(DateOnly.FromDateTime(cmd.BirthDate));
        item?.IsFavorite.Should().Be(cmd.IsFavorite);
        item?.PhoneNumbers.Should().HaveCount(cmd.PhoneNumberList.Count);
        item?.PhoneNumbers.Should().BeEquivalentTo(cmd.PhoneNumberList);
        item?.Emails.Should().HaveCount(cmd.EmailList.Count);
        item?.Emails.Should().BeEquivalentTo(cmd.EmailList);

        var contactTag = await FindAsync<ContactTag>(tag.Id, contactId);
        contactTag.Should().NotBeNull();
    }
}
