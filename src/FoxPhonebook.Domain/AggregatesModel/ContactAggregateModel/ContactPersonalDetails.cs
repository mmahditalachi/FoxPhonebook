using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel
{
    public class ContactPersonalDetails : ValueObject
    {
        public ContactPersonalDetails(string firstName, string lastName, string companyName)
        {
            FirstName = Guard.Against.InvalidNameInput(firstName, nameof(firstName));
            LastName = Guard.Against.InvalidNameInput(lastName, nameof(lastName));
            CompanyName = Guard.Against.InvalidNameInput(companyName, nameof(companyName));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return CompanyName;
        }
    }
}
