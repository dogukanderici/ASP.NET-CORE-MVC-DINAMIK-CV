using Business.Abstract;
using Core.Utilities.FileOperations;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Areas.User.Models;
using Portfolyo.Models;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-About-RolePolicy")]
    public class AboutController : Controller
    {
        private IAboutService _aboutService;
        private IFileOperationHelper _fileOperationHelper;
        public AboutController(IAboutService aboutService, IFileOperationHelper fileOperationHelper)
        {
            _aboutService = aboutService;
            _fileOperationHelper = fileOperationHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.v1 = "Düzenle";
            ViewBag.v2 = "Hakkımda";

            var result = _aboutService.GetById(1);

            if (!result.Success)
            {
                return RedirectToAction("Index", "ErrorPage"); // Hata View'i Yazılabilir.
            }

            var model = new AboutViewModel
            {
                About = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AboutViewModel aboutViewModel)
        {
            AboutValidator validator = new AboutValidator();
            ValidationResult validationResult = validator.Validate(aboutViewModel);

            if (validationResult.IsValid)
            {
                if (aboutViewModel.ProfilePicture != null)
                {
                    var fileOperationResult = await _fileOperationHelper.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = aboutViewModel.ProfilePicture,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    aboutViewModel.About.ImageUrl = fileOperationResult;
                }
                else
                {
                    aboutViewModel.About.ImageUrl = "login-bg.jpg";
                }

                var result = _aboutService.TUpdate(aboutViewModel.About);

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
