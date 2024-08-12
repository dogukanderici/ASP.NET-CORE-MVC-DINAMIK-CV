using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NotFound404()
        {
            ViewBag.Code = "404";
            ViewBag.Content = "İsteğinizi Gerçekleştirirken Bir Hata İle Karşılaştık!";
            return View();
        }
        public IActionResult Error404()
        {
            ViewBag.Code = "404";
            ViewBag.Content = "Aradığnız Sayfayı Bulamadık. Ana Sayafaya Dönerek Tekrar Deneyebilirsiniz!";
            return View();
        }
    }
}
