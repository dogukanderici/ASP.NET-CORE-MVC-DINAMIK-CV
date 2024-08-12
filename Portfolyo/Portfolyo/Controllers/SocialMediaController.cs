using Business.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-SocialMedia-RolePolicy")]
    public class SocialMediaController : Controller
    {
        private ISocialMediaService _socialMedaiService;

        public SocialMediaController(ISocialMediaService socialMedaiService)
        {
            _socialMedaiService = socialMedaiService;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Hesaplarım";
            ViewBag.v2 = "Sosyal Medya";

            var result = _socialMedaiService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddNewSocialMedia()
        {
            var model = new SocialMedia
            {
                Status = true
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddNewSocialMedia(SocialMedia socialMedia)
        {
            SocialMediaValidator validator = new SocialMediaValidator();
            ValidationResult validationResult = validator.Validate(socialMedia);

            if (validationResult.IsValid)
            {
                var result = _socialMedaiService.TAdd(socialMedia);

                if (!result.Success)
                {
                    return View(); // Hata View'i Yazılabilir.
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteSocialMedia(int id)
        {
            var result = _socialMedaiService.TDelete(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var result = _socialMedaiService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            SocialMediaValidator validator = new SocialMediaValidator();
            ValidationResult validationResult = validator.Validate(socialMedia);

            if (validationResult.IsValid)
            {
                var result = _socialMedaiService.TUpdate(socialMedia);

                if (!result.Success)
                {
                    return View(); // Hata View'i Yazılabilir.
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
