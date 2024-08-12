using Business.Abstract;
using Core.Utilities.Result;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]/{id?}")]
    [Authorize]
    public class MessageController : Controller
    {
        private IWriterMessageService _writerMessageService;
        private readonly UserManager<WriterUser> _userManager;

        public MessageController(IWriterMessageService writerMessageService, UserManager<WriterUser> userManager)
        {
            _writerMessageService = writerMessageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox()
        {
            var userData = await _userManager.FindByNameAsync(User.Identity.Name);
            string receiver = userData.Email;

            var result = _writerMessageService.TGetList(wm => wm.Receiver == receiver);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
        public async Task<IActionResult> Outbox()
        {
            var userData = await _userManager.FindByNameAsync(User.Identity.Name);
            string sender = userData.Email;

            var result = _writerMessageService.TGetList(wm => wm.Sender == sender);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        public IActionResult InboxMessageDetails(int id)
        {
            var result = _writerMessageService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            // Okundu Bilgisi Yazıdırılıyor.
            result.Data.ReceiverStatus = true;

            var updatedResult = _writerMessageService.TUpdate(result.Data);

            if (!updatedResult.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        public IActionResult OutboxMessageDetails(int id)
        {
            var result = _writerMessageService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(WriterMessage message)
        {
            var userData = await _userManager.FindByNameAsync(User.Identity.Name);
            string mail = userData.Email;

            message.Date = DateTime.Now;
            message.Sender = mail;
            message.SenderName = userData.Name;
            message.ReceiverStatus = false;
            message.SenderStatus = true;

            _writerMessageService.TAdd(message);

            return RedirectToAction("Outbox");
        }

        [HttpGet]
        public IActionResult ReplyMessage(int id)
        {
            var result = _writerMessageService.GetById(id);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            // Okundu Bilgisi Yazıdırılıyor.
            result.Data.ReceiverStatus = true;
            result.Data.SenderStatus = true;

            var updatedResult = _writerMessageService.TUpdate(result.Data);

            result.Data.MessageContent = "";
            result.Data.ReceiverName = result.Data.SenderName;
            result.Data.Receiver = result.Data.Sender;
            result.Data.SenderName = "";
            result.Data.Sender = "";

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ReplyMessage(WriterMessage message)
        {
            var userData = await _userManager.FindByNameAsync(User.Identity.Name);
            string mail = userData.Email;

            message.Date = DateTime.Now;
            message.Sender = mail;
            message.SenderName = userData.Name;
            message.ReceiverStatus = true;
            message.SenderStatus = true;

            _writerMessageService.TAdd(message);

            return RedirectToAction("Outbox");
        }
    }
}