using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class DefaultController : Controller
    {
        private IAnnouncementService _announcementService;

        public DefaultController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public IActionResult Index()
        {
            var result = _announcementService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AnnouncementDetails(int id)
        {
            var result = _announcementService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult AnnouncementDetails(Announcement announcement)
        {
            var result = _announcementService.TUpdate(announcement);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index", "Default");
        }
    }
}
