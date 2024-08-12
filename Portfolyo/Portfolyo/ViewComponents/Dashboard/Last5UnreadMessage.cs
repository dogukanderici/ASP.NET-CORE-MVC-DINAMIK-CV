using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Dashboard
{
    public class Last5UnreadMessage : ViewComponent
    {
        private IWriterMessageService _writerMessageService;
        private readonly UserManager<WriterUser> _userManager;

        public Last5UnreadMessage(IWriterMessageService writerMessageService, UserManager<WriterUser> userManager)
        {
            _writerMessageService = writerMessageService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userResult = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = _writerMessageService.GetLast5UnreadMessage(wm => wm.Receiver == userResult.Email && wm.SenderStatus == false && wm.ReceiverStatus == false);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
