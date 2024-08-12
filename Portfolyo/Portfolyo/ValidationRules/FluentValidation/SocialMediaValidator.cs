using Business.Contants;
using Entities.Concrete;
using FluentValidation;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class SocialMediaValidator : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidator()
        {
            RuleFor(sm => sm.Name).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(sm => sm.Url).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(sm => sm.Icon).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
        }
    }
}
