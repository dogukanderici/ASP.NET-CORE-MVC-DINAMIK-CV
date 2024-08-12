using Entities.Dtos;
using FluentValidation;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Ad Alanı Boş Bırakılamaz");
            RuleFor(r => r.Surname).NotEmpty().WithMessage("Soyad Alanı Boş Bırakılamaz");
            RuleFor(r => r.Username).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz");
            RuleFor(r => r.ImageUrl).NotEmpty().WithMessage("Görsel URL Alanı Boş Bırakılamaz");
            RuleFor(r => r.Email).NotEmpty().WithMessage("E-Posta Alanı Boş Bırakılamaz");
            RuleFor(r => r.ConfirmEmail).NotEmpty().WithMessage("E-Posta Alanı Boş Bırakılamaz");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Şifre Alanı Boş Bırakılamaz");
            RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Şifre Alanı Boş Bırakılamaz");

            RuleFor(r => r.ConfirmPassword).Equal(r => r.Password).WithMessage("Şifreleriniz Aynı Değil");
            RuleFor(r => r.ConfirmEmail).Equal(r => r.Email).WithMessage("E-Posta Adresleri Aynı Değil");

        }
    }
}
