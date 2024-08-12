using Business.Contants;
using Entities.Concrete;
using FluentValidation;
using Portfolyo.Models;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class ServiceValidator : AbstractValidator<ServiceViewModel>
    {
        public ServiceValidator()
        {
            RuleFor(s => s.Service.Title).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(s => s.ServicePicture).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
        }
    }
}
