using Entities.Concrete;
using FluentValidation;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class FeatureValidator : AbstractValidator<Feature>
    {
        public FeatureValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("İsim Alanı Boş Bırakılamaz!");
            RuleFor(f => f.Title).NotEmpty().WithMessage("İçerik Alanı Boş Bırakılamaz!");
            RuleFor(f => f.Header).NotEmpty().WithMessage("Başlık Alanı Boş Bırakılamaz!");

            RuleFor(f => f.Name).MinimumLength(5).WithMessage("İsim Alanı En Az 5 Karakterden Oluşmalıdır!");
            RuleFor(f => f.Title).MinimumLength(20).WithMessage("İçerik Alanı En Az 20 Karakterden Oluşmalıdır!");
            RuleFor(f => f.Header).MinimumLength(5).WithMessage("Başlık Alanı En Az 5 Karakterden Oluşmalıdır!");
        }
    }
}
