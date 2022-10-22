using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;

namespace FoxPhonebook.Domain.Test.ContactAggregateTests
{
    public class ContactPersonalDetailsTest
    {
        [Test]
        public void ShouldRemoveSpecialCharecterFromFirstNameAndLastName()
        {
            // Arrange
            string firstName = "moha@mmad";
            string lastName = "tala@ch$&&*#i";
            string companyName = "dddd";

            // Act
            var contact = new ContactPersonalDetails(firstName, lastName, companyName);

            // Assert
            contact.FirstName.Should().Be("mohammad");
            contact.LastName.Should().Be("talachi");
            contact.CompanyName.Should().Be(companyName);
        }

        [Test]
        public void ShouldRemoveMoreThanOneWhiteSpaceFromFirstNameAndLastName()
        {
            // Arrange
            string firstName = "mohammad";
            string lastName = "talachi  zade";
            string companyName = "dddd";

            // Act
            var contact = new ContactPersonalDetails(firstName, lastName, companyName);

            // Assert
            contact.FirstName.Should().Be("mohammad");
            contact.LastName.Should().Be("talachi zade");
            contact.CompanyName.Should().Be(companyName);
        }

        [Test]
        public void ShouldConvertToFirstNameAndLastNameLowercase()
        {
            // Arrange
            string firstName = "MohAmmad";
            string lastName = "TaLACHi";
            string companyName = "DDp";

            // Act
            var contact = new ContactPersonalDetails(firstName, lastName, companyName);

            // Assert
            contact.FirstName.Should().Be("mohammad");
            contact.LastName.Should().Be("talachi");
            contact.CompanyName.Should().Be("ddp");
        }

        [Test]
        [TestCase("mohammad", "", "ddd")]
        [TestCase("", "talachi", "ddd")]
        [TestCase("mohammad", "talachi", "")]
        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        public void ShouldThrowArgumentExceptionWhenFirstNameOrLastNameOrCompanyNameIsEmptyOrNull(
            string firstName, string lastName, string companyName)
        {
            FluentActions.Invoking(() =>
            {
                var contact = new ContactPersonalDetails(firstName, lastName, companyName);
            }).Should().Throw<ArgumentException>();
        }
    }
}
