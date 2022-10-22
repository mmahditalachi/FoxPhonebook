using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task StartupSeed(ApplicationDbContext context)
        {
            if (context.Contacts.Any())
                return;

            var tag_one = new Tag("school");
            var tag_two = new Tag("DFL");
            var tag_three = new Tag("overwatch");

            var contact_one = new Contact(new ContactPersonalDetails("mohammad", "talachi", "microsoft"), new DateOnly(1999, 11, 24), true);
            contact_one.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_one.AddOrUpdatePhoneNumber(new ContactPhoneNumber("mobile", "09127000000"));
            contact_one.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "02122884493"));

            var contact_two = new Contact(new ContactPersonalDetails("ali", "talachi", "google"), new DateOnly(1996, 5, 6), false);
            contact_two.AddOrUpdateContactEmail(new ContactEmail("test@test.com"));
            contact_two.AddOrUpdatePhoneNumber(new ContactPhoneNumber("home", "02122889541"));

            var contact_three = new Contact(new ContactPersonalDetails("melica", "zahedi", "microsoft"), new DateOnly(1999, 5, 24), true);
            contact_two.AddOrUpdateContactEmail(new ContactEmail("melica@gmail.com"));
            contact_two.AddOrUpdatePhoneNumber(new ContactPhoneNumber("mobile", "09128000000"));

            var contactTagList = new[]{
                new ContactTag(contact_one.Id, tag_one.Id),
                new ContactTag(contact_two.Id, tag_one.Id),
                new ContactTag(contact_two.Id, tag_two.Id),
                new ContactTag(contact_three.Id, tag_one.Id),
                new ContactTag(contact_three.Id, tag_three.Id),
            };

            context.Tags.AddRange(tag_one, tag_two, tag_three);
            context.ContactTags.AddRange(contactTagList);
            context.Contacts.AddRange(contact_one, contact_two, contact_three);

            await context.SaveChangesAsync();
        }
    }
}
