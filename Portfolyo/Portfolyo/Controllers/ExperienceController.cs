using Business.Abstract;
using Core.Utilities.FileOperations;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Models;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-Experience-RolePolicy")]
    public class ExperienceController : Controller
    {
        private IExperienceService _experieneceService;
        private IFileOperationHelper _fileOperation;
        public ExperienceController(IExperienceService experienceService, IFileOperationHelper fileOperation)
        {
            _experieneceService = experienceService;
            _fileOperation = fileOperation;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Deneyim Listesi";
            ViewBag.v2 = "Deneyimler";

            var result = _experieneceService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata Vİew'i Yazılabilir.
            }

            var model = new ExperienceListViewModel
            {
                Experiences = result.Data
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNewExperience()
        {
            ViewBag.v1 = "Deneyim Listesi";
            ViewBag.v2 = "Deneyimler";

            var model = new ExperienceListViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExperience(ExperienceListViewModel experienceListViewModel)
        {
            ExperienceValidator validator = new ExperienceValidator();
            ValidationResult validationResult = validator.Validate(experienceListViewModel);

            if (validationResult.IsValid)
            {
                if (experienceListViewModel.ExperienceImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = experienceListViewModel.ExperienceImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    experienceListViewModel.Experience.ImageUrl = fileOperationResult;
                }

                var result = _experieneceService.TAdd(experienceListViewModel.Experience);

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

        public IActionResult DeleteExperience(int id)
        {
            var result = _experieneceService.TDelete(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            ViewBag.v1 = "Deneyimler";
            ViewBag.v2 = "Deneyim Düzenleme";

            var result = _experieneceService.GetById(id);

            var model = new ExperienceListViewModel
            {
                Experience = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExperience(ExperienceListViewModel experienceListViewModel)
        {
            ExperienceValidator validator = new ExperienceValidator();
            ValidationResult validationResult = validator.Validate(experienceListViewModel);

            if (validationResult.IsValid)
            {
                if (experienceListViewModel.ExperienceImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = experienceListViewModel.ExperienceImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    experienceListViewModel.Experience.ImageUrl = fileOperationResult;
                }

                var result = _experieneceService.TUpdate(experienceListViewModel.Experience);

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
