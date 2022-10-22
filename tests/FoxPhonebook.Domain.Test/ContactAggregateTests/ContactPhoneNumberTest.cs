using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.Test.ContactAggregateTests
{
    public class ContactPhoneNumberTest
    {
        [Test]
        public void ShouldRemoveSpecialCharecterFromTitle()
        {
            // Arrange
            string title = "h@#$ome";
            string phoneNumber = "09126000000";

            // Act
            var contactPhone = new ContactPhoneNumber(title, phoneNumber);
            
            // Assert
            contactPhone.Title.Should().Be("home");
            contactPhone.PhoneNumber.Should().Be("09126000000");
        }

        [Test]
        public void ShouldThrowArgumentException_WhenPhoneNumberContainText()
        {
            // Arrange
            string title = "home";
            string phoneNumber = "09sd12sds6sds0sds00fd000";

            // Act and Assert
            FluentActions.Invoking(() => {
                var contactPhone = new ContactPhoneNumber(title, phoneNumber);
            }).Should().Throw<ArgumentException>();
        }
    }
}
