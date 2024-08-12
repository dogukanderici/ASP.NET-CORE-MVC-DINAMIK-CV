using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Areas.User.ViewComponents.Announcement
{
    [Authorize]
    public class AnnouncementList : ViewComponent
    {
        private IAnnouncementService _announcementService;

        public AnnouncementList(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _announcementService.GetLastFiveDataList();
            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
