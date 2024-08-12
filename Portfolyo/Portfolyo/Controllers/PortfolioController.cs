using Business.Abstract;
using Core.Utilities.FileOperations;
using Core.Utilities.Result;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Models;
using Portfolyo.ValidationRules.FluentValidation;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-Portfolio-RolePolicy")]
    public class PortfolioController : Controller
    {
        private IPortfolioService _portfolioService;
        private IFileOperationHelper _fileOperation;
        public PortfolioController(IPortfolioService portfolioService, IFileOperationHelper fileOperation)
        {
            _portfolioService = portfolioService;
            _fileOperation = fileOperation;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Projeler";
            ViewBag.v2 = "Proje Listesi";

            var result = _portfolioService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            var model = new PortfolioListViewModel
            {
                Portfolios = result.Data
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNewPortfolio()
        {
            ViewBag.v1 = "Projeler";
            ViewBag.v2 = "Yeni Proje Ekle";

            var model = new PortfolioListViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPortfolio(PortfolioListViewModel portfolioListViewModel)
        {

            PortfolioValidator validator = new PortfolioValidator();
            ValidationResult validationResult = validator.Validate(portfolioListViewModel);

            if (validationResult.IsValid)
            {
                if (portfolioListViewModel.ProjectImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.ProjectImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.ImageUrl = fileOperationResult;
                }

                if (portfolioListViewModel.PlatformImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.PlatformImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Platform = fileOperationResult;
                }

                if (portfolioListViewModel.ProjectImage2 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.ProjectImage2,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.ImageUrl2 = fileOperationResult;
                }

                if (portfolioListViewModel.Image1 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image1,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image1 = fileOperationResult;
                }

                if (portfolioListViewModel.Image2 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image2,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image2 = fileOperationResult;
                }

                if (portfolioListViewModel.Image3 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image3,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image3 = fileOperationResult;
                }

                if (portfolioListViewModel.Image4 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image4,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image4 = fileOperationResult;
                }

                var result = _portfolioService.TAdd(portfolioListViewModel.Portfolio);

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

        public IActionResult DeletePortfolio(int id)
        {
            var result = _portfolioService.TDelete(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdatePortfolio(int id)
        {
            ViewBag.v1 = "Projeler";
            ViewBag.v2 = "Proje Güncelleme";

            var result = _portfolioService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            var model = new PortfolioListViewModel
            {
                Portfolio = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePortfolio(PortfolioListViewModel portfolioListViewModel)
        {
            PortfolioValidator validator = new PortfolioValidator();
            ValidationResult validatorResult = validator.Validate(portfolioListViewModel);

            if (validatorResult.IsValid)
            {
                if (portfolioListViewModel.ProjectImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.ProjectImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.ImageUrl = fileOperationResult;
                }

                if (portfolioListViewModel.PlatformImage != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.PlatformImage,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Platform = fileOperationResult;
                }

                if (portfolioListViewModel.ProjectImage2 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.ProjectImage2,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.ImageUrl2 = fileOperationResult;
                }

                if (portfolioListViewModel.Image1 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image1,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image1 = fileOperationResult;
                }

                if (portfolioListViewModel.Image2 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image2,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image2 = fileOperationResult;
                }

                if (portfolioListViewModel.Image3 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image3,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image3 = fileOperationResult;
                }

                if (portfolioListViewModel.Image4 != null)
                {
                    var fileOperationResult = await _fileOperation.SaveFileToFolder(new FileProperty
                    {
                        UserAddedFile = portfolioListViewModel.Image4,
                        UserAddedFilePath = "/wwwroot/userfile/"
                    });

                    portfolioListViewModel.Portfolio.Image4 = fileOperationResult;
                }

                var result = _portfolioService.TUpdate(portfolioListViewModel.Portfolio);

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
