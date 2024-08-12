using Entities.Dtos;
using FluentValidation;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(l => l.Email).NotEmpty().WithMessage("E-Posta Boş Olamaz");
            RuleFor(l => l.Password).NotEmpty().WithMessage("Şifre Boş Olamaz");
        }
    }
}
