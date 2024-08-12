using Business.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-Feature-RolePolicy")]
    public class FeatureController : Controller
    {
        private IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.v1 = "Öne Çıkanlar";
            ViewBag.v2 = "Düzenle";

            var result = _featureService.GetById(1);

            if (!result.Success)
            {
                return RedirectToAction("Index", "ErrorPage"); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Index(Feature feature)
        {
            FeatureValidator validator = new FeatureValidator();
            ValidationResult validationResult = validator.Validate(feature);

            if (validationResult.IsValid)
            {
                var result = _featureService.TUpdate(feature);

                if (!result.Success)
                {
                    return RedirectToAction("Index", "ErrorPage"); // Hata View'i Yazılabilir.
                }

                return RedirectToAction("Index", "Default");
            }
            else
            {
                return View();
            }

        }

    }
}
