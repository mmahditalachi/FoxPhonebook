using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.Test.ContactAggregateTests
{
    public class ContactEmailTest
    {

        [Test]
        [TestCaseSource(nameof(ValidEmails))]
        public void ShouldCreateContactEmail(string email)
        {
            // Arrenge

            // Act
            var contactEmail = new ContactEmail(email);

            // Assert
            contactEmail.Email.Should().Be(email);

        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowArgumentException_WhenEmailIsEmptyOrNull(string email)
        {
            FluentActions.Invoking(() =>
            {
                var contactEmail = new ContactEmail(email);
            }).Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmailList))]
        public void ShouldThrowArgumentException_WhenEmailIsInvalid(string email)
        {
            FluentActions.Invoking(() =>
            {
                var contactEmail = new ContactEmail(email);
            }).Should().Throw<ArgumentException>().WithMessage("invalid email (Parameter 'email')");
        }

        static object[] InvalidEmailList =
        {
            new object[] { "email.@example.com"},
            new object[] { "email..email@example.com"},
            new object[] { "email@example"},
            new object[] { "email@-example.com"},
            new object[] { "email@111.222.333.44444"},
            new object[]{ "email@example.com (Joe Smith)" }
        };

        static object[] ValidEmails =
        {
            new object[] {"email@subdomain.example.com"},
            new object[] {"1234567890@example.com"},
            new object[] {"email@example.co.jp"},
            new object[] {"firstname-lastname@example.com"},
        };
    }
}
