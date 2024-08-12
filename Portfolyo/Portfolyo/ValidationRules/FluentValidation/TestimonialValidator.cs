using Business.Contants;
using Entities.Concrete;
using FluentValidation;
using Portfolyo.Models;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class TestimonialValidator : AbstractValidator<TestimonialListViewModel>
    {
        public TestimonialValidator()
        {
            RuleFor(t => t.Testimonial.ClientName).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(t => t.Testimonial.Company).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(t => t.Testimonial.Comment).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            //RuleFor(t => t.TestimonialImage).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(t => t.Testimonial.Title).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
        }
    }
}
