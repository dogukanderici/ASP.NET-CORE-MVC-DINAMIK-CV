using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Portfolyo.Controllers
{
    public class WriterUserController : Controller
    {
        private IWriterUserService _writerUserService;

        public WriterUserController(IWriterUserService writerUserService)
        {
            _writerUserService = writerUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListUser()
        {
            var result = _writerUserService.TGetList();

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var values = JsonConvert.SerializeObject(result.Data);

            return Json(values);
        }

        [HttpPost]
        public IActionResult AddUser(WriterUser writerUser)
        {
            var result = _writerUserService.TAdd(writerUser);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var values = JsonConvert.SerializeObject(writerUser);

            return Json(values);
        }
    }
}
