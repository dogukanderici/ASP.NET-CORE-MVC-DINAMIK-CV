using Business.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Models;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-Skill-RolePolicy")]
    public class SkillController : Controller
    {
        private ISkillService _skillService;
        public SkillController(ISkillService skillController)
        {
            _skillService = skillController;
        }
        public IActionResult Index()
        {
            ViewBag.v1 = "Yetenek Listesi";
            ViewBag.v2 = "Yetenekler";
            var result = _skillService.TGetList();

            if (!result.Success)
            {
                return View();
            }

            var model = new SkillListViewModel
            {
                Skills = result.Data
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNewSkill()
        {
            ViewBag.v1 = "Yeni Yetenek Ekleme";
            ViewBag.v2 = "Yetenekeler";

            return View();
        }

        [HttpPost]
        public IActionResult AddNewSkill(Skill skill)
        {
            SkillValidator validator = new SkillValidator();
            ValidationResult validationResult = validator.Validate(skill);

            if (validationResult.IsValid)
            {
                var result = _skillService.TAdd(skill);

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

        public IActionResult DeleteSkill(int id)
        {
            var result = _skillService.TDelete(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            ViewBag.v1 = "Yetenek Güncelleme";
            ViewBag.v2 = "Yetenekeler";

            var result = _skillService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            SkillValidator validator = new SkillValidator();
            ValidationResult validationResult = validator.Validate(skill);

            if (validationResult.IsValid)
            {
                var result = _skillService.TUpdate(skill);

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
