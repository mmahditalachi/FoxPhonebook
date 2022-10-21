using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .GreaterThan(DateOnly.MinValue)
                .LessThan(DateOnly.MaxValue);

            RuleFor(e => e.PhoneNumbers)
                .NotNull()
                .Must(e => e.Count > 0);
        }
    }
}
