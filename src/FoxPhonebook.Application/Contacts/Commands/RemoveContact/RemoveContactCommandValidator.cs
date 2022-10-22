using FluentValidation;

namespace FoxPhonebook.Application.Contacts.Commands.RemoveContact
{
    public class RemoveContactCommandValidator : AbstractValidator<RemoveContactCommand>
    {
        public RemoveContactCommandValidator()
        {
            RuleFor(e => e.ContactId)
                .NotEmpty();
        }
    }
}
