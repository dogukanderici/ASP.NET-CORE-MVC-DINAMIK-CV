using Business.Contants;
using Entities.Concrete;
using FluentValidation;
using Portfolyo.Models;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class AboutValidator : AbstractValidator<AboutViewModel>
    {
        public AboutValidator()
        {
            RuleFor(a => a.About.Title).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(a => a.About.Description).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(a => a.About.Age).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(a => a.About.Mail).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(a => a.About.Phone).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(a => a.About.Address).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            //RuleFor(a => a.ProfilePicture).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);

            RuleFor(a => a.About.Age).GreaterThanOrEqualTo(18).WithMessage("18 veya Daha büyük Bir Yaş Girilmelidir!");
            RuleFor(a => a.About.Mail).EmailAddress().WithMessage(ValidatorMessages.NotValidMailMessage);
        }
    }
}
