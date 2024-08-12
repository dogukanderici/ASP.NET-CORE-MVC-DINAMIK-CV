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
    [Authorize(Policy = "Dynamic-Testimonial-RolePolicy")]
    public class TestimonialController : Controller
    {
        private ITestimonialService _testimonialService;
        private IFileOperationHelper _fileOperation;

        public TestimonialController(ITestimonialService testimonialService, IFileOperationHelper fileOperation)
        {
            _testimonialService = testimonialService;
            _fileOperation = fileOperation;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Referans";
            ViewBag.v2 = "Referanslarım";

            var result = _testimonialService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddTestimonial()
        {
            var model = new TestimonialListViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTestimonial(TestimonialListViewModel testimonialListViewModel)
        {
            TestimonialValidator validator = new TestimonialValidator();
            ValidationResult validationResult = validator.Validate(testimonialListViewModel);

            if (validationResult.IsValid)
            {
                if (testimonialListViewModel.TestimonialImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = testimonialListViewModel.TestimonialImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    testimonialListViewModel.Testimonial.ImageUrl = fileOperationResult;
                }
                else
                {
                    testimonialListViewModel.Testimonial.ImageUrl = "pic-8.png";
                }

                var result = _testimonialService.TAdd(testimonialListViewModel.Testimonial);
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

        public IActionResult DeleteTestimonial(int id)
        {
            var result = _testimonialService.TDelete(id);
            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var result = _testimonialService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            var model = new TestimonialListViewModel
            {
                Testimonial = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(TestimonialListViewModel testimonialListViewModel)
        {
            TestimonialValidator validator = new TestimonialValidator();
            ValidationResult validationResult = validator.Validate(testimonialListViewModel);

            if (validationResult.IsValid)
            {
                if (testimonialListViewModel.TestimonialImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = testimonialListViewModel.TestimonialImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    testimonialListViewModel.Testimonial.ImageUrl = fileOperationResult;
                }
                else
                {
                    testimonialListViewModel.Testimonial.ImageUrl = "pic-8.png";
                }

                var result = _testimonialService.TUpdate(testimonialListViewModel.Testimonial);
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
