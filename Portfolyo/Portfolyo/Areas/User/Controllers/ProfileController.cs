using Core.Utilities.FileOperations;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Areas.User.Models;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "Dynamic-Profile-RolePolicy")]
    public class ProfileController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;
        private IFileOperationHelper _fileOperationHelper;

        public ProfileController(UserManager<WriterUser> userManager, IFileOperationHelper fileOperationHelper)
        {
            _userManager = userManager;
            _fileOperationHelper = fileOperationHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new UserEditViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                PictureURL = user.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel userEditViewModel)
        {
            UserEditValidator validator = new UserEditValidator();
            ValidationResult validationResult = validator.Validate(userEditViewModel);

            if (validationResult.IsValid)
            {
                var result = await _userManager.FindByNameAsync(User.Identity.Name);

                if (userEditViewModel.Picture != null)
                {
                    //var resource = Directory.GetCurrentDirectory();
                    //var extension = Path.GetExtension(userEditViewModel.Picture.FileName);
                    //var imageName = Guid.NewGuid() + extension;
                    //var saveLocation = resource + "/wwwroot/userimage/" + imageName;
                    //var stream = new FileStream(saveLocation, FileMode.Create);

                    //await userEditViewModel.Picture.CopyToAsync(stream);

                    var fileOperationResult = await _fileOperationHelper.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = userEditViewModel.Picture,
                        UserAddedFilePath = "/wwwroot/userimage/"
                    });

                    result.ImageUrl = fileOperationResult;
                }

                result.Name = userEditViewModel.Name;
                result.Surname = userEditViewModel.Surname;
                result.PasswordHash = _userManager.PasswordHasher.HashPassword(result, userEditViewModel.Password);

                var updatedUserResult = await _userManager.UpdateAsync(result);

                if (updatedUserResult.Succeeded)
                {
                    return RedirectToAction("Login", "Auth", new { area = "" });
                }
                else
                {
                    foreach (var item in updatedUserResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

                return View();
            }
            else
            {
                return View();
            }
        }
    }
}