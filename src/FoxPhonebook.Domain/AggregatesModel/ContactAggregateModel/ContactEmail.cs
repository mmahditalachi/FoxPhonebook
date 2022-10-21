using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel
{
    public class ContactEmail : ValueObject
    {
        public ContactEmail(string email)
        {
            Guard.Against.NullOrEmpty(email);
            Email = Guard.Against.InvalidFormat(email, nameof(email), Utility.EmailRegexPattern, "invalid email");
        }

        public string Email { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}
