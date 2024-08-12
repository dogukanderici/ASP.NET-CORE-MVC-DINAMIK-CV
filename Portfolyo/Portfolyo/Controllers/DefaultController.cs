using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private IMessageService _messageService;
        public DefaultController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }

        public PartialViewResult NavBarPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult SendMessage(WriterMessage message)
        {
            message.Date = Convert.ToDateTime(DateTime.Now);
            message.ReceiverStatus = false;
            message.SenderStatus = false;
            message.Receiver = "admin.test@gmail.com";
            message.ReceiverName = "admin.test@gmail.com";

            _messageService.TAdd(message);

            return PartialView("Index", "Default");
        }


    }
}
