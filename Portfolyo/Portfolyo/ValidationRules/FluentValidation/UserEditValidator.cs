using FluentValidation;
using Portfolyo.Areas.User.Models;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class UserEditValidator : AbstractValidator<UserEditViewModel>
    {
        public UserEditValidator()
        {
            RuleFor(ue => ue.Name).NotEmpty().WithMessage("Bu Alan Boş Bırakılamaz!");
            RuleFor(ue => ue.Surname).NotEmpty().WithMessage("Bu Alan Boş Bırakılamaz!");
            RuleFor(ue => ue.Password).NotEmpty().WithMessage("Bu Alan Boş Bırakılamaz!");
            RuleFor(ue => ue.ConfirmPassword).NotEmpty().WithMessage("Bu Alan Boş Bırakılamaz!");
            RuleFor(ue => ue.Picture).NotEmpty().WithMessage("Bu Alan Boş Bırakılamaz!");

        }
    }
}
