﻿using FoxPhonebook.Application.Common.Exceptions;
using FoxPhonebook.Application.Contacts.Commands.CreateContact;
using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Application.IntegrationTests.Contacts.Commands;

using static Testing;

public class CreateContactTest : TestBase
{
    [Test]
    public async Task ShouldCreateContact_WhenTagExist()
    {
        // Arrenge
        var tag = new Tag("school");
        await AddAsync(tag);

        var cmd = new CreateContactCommand()
        {
            FirstName = "Mohammad",
            LastName = "Talachi",
            CompanyName = "DDD",
            BirthDate = new DateOnly(1999, 11, 24),
            EmailList = new List<ContactEmail> { new ContactEmail("mohammadmahditalachi@gmail.com") },
            IsFavorite = true,
            PhoneNumbers = new List<ContactPhoneNumber>
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
        item?.BirthDate.Should().Be(cmd.BirthDate);
        item?.IsFavorite.Should().Be(cmd.IsFavorite);
        item?.PhoneNumbers.Should().HaveCount(cmd.PhoneNumbers.Count);
        item?.PhoneNumbers.Should().BeEquivalentTo(cmd.PhoneNumbers);
        item?.Emails.Should().HaveCount(cmd.EmailList.Count);
        item?.Emails.Should().BeEquivalentTo(cmd.EmailList);

        var contactTag = await FindAsync<ContactTag>(tag.Id, contactId);
        contactTag.Should().NotBeNull();        
    }

    [Test]
    public async Task ShouldNotCreateContact_WhenPhoneNumberIsEmpty()
    {
        // Arrenge
        var tag = new Tag("school");
        await AddAsync(tag);

        var cmd = new CreateContactCommand()
        {
            FirstName = "Mohammad",
            LastName = "Talachi",
            CompanyName = "DDD",
            BirthDate = new DateOnly(1999, 11, 24),
            EmailList = new List<ContactEmail> { new ContactEmail("mohammadmahditalachi@gmail.com") },
            IsFavorite = true,
            TagIdList = new List<Guid> { tag.Id },
        };

        // Act and Assert
        await FluentActions.Invoking(() =>
                 SendAsync(cmd)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateContact_WhenTagNotExist()
    {
        // Arrenge
        var cmd = new CreateContactCommand()
        {
            FirstName = "Mohammad",
            LastName = "Talachi",
            CompanyName = "DDD",
            BirthDate = new DateOnly(1999, 11, 24),
            EmailList = new List<ContactEmail> { new ContactEmail("mohammadmahditalachi@gmail.com") },
            IsFavorite = true,
            PhoneNumbers = new List<ContactPhoneNumber>
            {
                new ContactPhoneNumber("home", "22222222"),
                new ContactPhoneNumber("mobile", "09127000000"),
            },
        };

        // Act
        var contactId = await SendAsync(cmd);

        // Assert
        var item = await FindAsync<Contact>(contactId);
        item.Should().NotBeNull();
        item?.PersonalDetails.FirstName.Should().Be(cmd.FirstName.ToLower());
        item?.PersonalDetails.LastName.Should().Be(cmd.LastName.ToLower());
        item?.PersonalDetails.CompanyName.Should().Be(cmd.CompanyName.ToLower());
        item?.BirthDate.Should().Be(cmd.BirthDate);
        item?.IsFavorite.Should().Be(cmd.IsFavorite);
        item?.PhoneNumbers.Should().HaveCount(cmd.PhoneNumbers.Count);
        item?.PhoneNumbers.Should().BeEquivalentTo(cmd.PhoneNumbers);
        item?.Emails.Should().HaveCount(cmd.EmailList.Count);
        item?.Emails.Should().BeEquivalentTo(cmd.EmailList);

        var contactTagCount = await CountAsync<ContactTag>();
        contactTagCount.Should().Be(0);
    }
}