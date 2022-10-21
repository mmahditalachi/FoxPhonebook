using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel
{
    public class ContactPhoneNumber : ValueObject
    {
        public ContactPhoneNumber(string title, string phoneNumber)
        {
            Title = Guard.Against.InvalidNameInput(title, nameof(title));

            PhoneNumber = Guard.Against.InvalidInput(phoneNumber, nameof(phoneNumber), e => {

                phoneNumber = phoneNumber.ReplaceSpecialCharacter()
                    .ReplaceMoreThanOneSpace();

                return phoneNumber.All(char.IsDigit);

            }, "invalid phone number");
        }

        public string Title { get; init; }
        public string PhoneNumber { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return PhoneNumber;
        }
    }
}
