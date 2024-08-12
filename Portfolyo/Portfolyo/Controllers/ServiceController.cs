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
    [Authorize(Policy = "Dynamic-Service-RolePolicy")]
    public class ServiceController : Controller
    {
        private IServiceService _serviceService;
        private IFileOperationHelper _fileOperation;
        public ServiceController(IServiceService serviceService, IFileOperationHelper fileOperation)
        {
            _serviceService = serviceService;
            _fileOperation = fileOperation;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.v1 = "Düzenle";
            ViewBag.v2 = "Hizmetlerim";

            var result = _serviceService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            var model = new ServiceListViewModel
            {
                Services = result.Data
            };

            return View(model);
        }

        public IActionResult DeleteService(int id)
        {
            var result = _serviceService.TDelete(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateService(int id)
        {
            ViewBag.v1 = "Güncelle";
            ViewBag.v2 = "Hizmetlerim";

            var result = _serviceService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            var model = new ServiceViewModel
            {
                Service = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(ServiceViewModel serviceViewModel)
        {
            ServiceValidator validator = new ServiceValidator();
            ValidationResult validationResult = validator.Validate(serviceViewModel);

            if (validationResult.IsValid)
            {
                if (serviceViewModel.ServicePicture != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = serviceViewModel.ServicePicture,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    serviceViewModel.Service.ImageUrl = fileOperationResult;
                }

                var result = _serviceService.TUpdate(serviceViewModel.Service);

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

        [HttpGet]
        public IActionResult AddNewService()
        {

            ViewBag.v1 = "Yeni Hizmet Ekle";
            ViewBag.v2 = "Hizmetlerim";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewService(ServiceViewModel serviceViewModel)
        {
            ServiceValidator validator = new ServiceValidator();
            ValidationResult validationResult = validator.Validate(serviceViewModel);

            if (validationResult.IsValid)
            {
                if (serviceViewModel.ServicePicture != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = serviceViewModel.ServicePicture,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    serviceViewModel.Service.ImageUrl = fileOperationResult;
                }

                var result = _serviceService.TAdd(serviceViewModel.Service);

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
