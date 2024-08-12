using Business.Contants;
using Entities.Concrete;
using FluentValidation;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class ContactSubplaceValidator : AbstractValidator<Contact>
    {
        public ContactSubplaceValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(c => c.Description).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(c => c.Mail).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(c => c.Phone).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
        }
    }
}
