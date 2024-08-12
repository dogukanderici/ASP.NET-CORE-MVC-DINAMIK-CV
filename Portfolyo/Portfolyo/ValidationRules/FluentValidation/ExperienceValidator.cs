using Business.Contants;
using Entities.Concrete;
using FluentValidation;
using Portfolyo.Models;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class ExperienceValidator : AbstractValidator<ExperienceListViewModel>
    {
        public ExperienceValidator()
        {
            RuleFor(e => e.Experience.Name).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(e => e.Experience.Date).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            //RuleFor(e => e.ExperienceImage).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(e => e.Experience.Description).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
        }
    }
}
