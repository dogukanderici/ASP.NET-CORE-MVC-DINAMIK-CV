using Business.Contants;
using Entities.Concrete;
using FluentValidation;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class SkillValidator : AbstractValidator<Skill>
    {
        public SkillValidator()
        {
            RuleFor(s => s.Title).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);
            RuleFor(s => s.Value).NotEmpty().WithMessage(ValidatorMessages.NotNullMessage);

            RuleFor(s => s.Value).Must(ValueControl).WithMessage("0'dan (sıfır) ve Daha Büyük Bir Değer Giriniz!");
        }

        private bool ValueControl(string arg)
        {
            var intValuee = Convert.ToInt32(arg);

            return intValuee >= 0;
        }
    }
}
