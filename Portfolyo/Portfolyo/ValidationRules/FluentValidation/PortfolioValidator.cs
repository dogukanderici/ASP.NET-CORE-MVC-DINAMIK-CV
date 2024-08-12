using Entities.Concrete;
using FluentValidation;
using Portfolyo.Models;

namespace Portfolyo.ValidationRules.FluentValidation
{
    public class PortfolioValidator : AbstractValidator<PortfolioListViewModel>
    {
        public PortfolioValidator()
        {
            RuleFor(p => p.Portfolio.Name).NotEmpty().WithMessage("Proje Adı Boş Geçilemez!");
            RuleFor(p => p.ProjectImage).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez!");
            RuleFor(p => p.ProjectImage2).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez!");
            RuleFor(p => p.Image1).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez!");
            RuleFor(p => p.Image2).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez!");
            RuleFor(p => p.Image3).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez!");
            RuleFor(p => p.Image4).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez!");
            RuleFor(p => p.Portfolio.Price).NotEmpty().WithMessage("Maliyet Alanı Boş Geçilemez!");
            RuleFor(p => p.Portfolio.Value).NotEmpty().WithMessage("Değer Alanı Boş Geçilemez!");
            RuleFor(p => p.Portfolio.ProjectUrl).NotEmpty().WithMessage("Proje Linki Alanı Boş Geçilemez!");
            RuleFor(p => p.PlatformImage).NotEmpty().WithMessage("Platform Alanı Boş Geçilemez!");
            RuleFor(p => p.Portfolio.State).NotEmpty().WithMessage("Proje Durumu Alanı Boş Geçilemez!");

            RuleFor(p => p.Portfolio.Name).MinimumLength(5).WithMessage("Proje Adı En Az 5 Karakter Olmalıdır!");
            RuleFor(p => p.Portfolio.Name).MaximumLength(100).WithMessage("Proje Adı En Fazla 100 Karakter Olablir!");
        }
    }
}
