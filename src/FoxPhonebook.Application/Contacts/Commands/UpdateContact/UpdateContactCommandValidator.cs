using FluentValidation;

namespace FoxPhonebook.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty();

            RuleFor(e => e.FirstName)
                .NotEmpty()
                .MaximumLength(75);

            RuleFor(e => e.LastName)
                .NotEmpty()
                .MaximumLength(75);

            RuleFor(e => e.CompanyName)
                .NotEmpty()
                .MaximumLength(75);

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.MaxValue);

            RuleFor(e => e.PhoneNumberList)
                .NotNull()
                .Must(e => e.Count > 0);
        }
    }
}
