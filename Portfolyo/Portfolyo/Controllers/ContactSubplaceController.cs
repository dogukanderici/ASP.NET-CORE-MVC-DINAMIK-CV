using Business.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-Contact-RolePolicy")]
    public class ContactSubplaceController : Controller
    {
        private IContactService _contactService;

        public ContactSubplaceController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _contactService.GetById(1);

            if (!result.Success)
            {
                return RedirectToAction("Index", "ErrorPage"); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            ContactSubplaceValidator validator = new ContactSubplaceValidator();
            ValidationResult validationResult = validator.Validate(contact);

            if (validationResult.IsValid)
            {
                var result = _contactService.TUpdate(contact);

                if (!result.Success)
                {
                    return RedirectToAction("Index", "ErrorPage"); // Hata View'i Yazılabilir.
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
