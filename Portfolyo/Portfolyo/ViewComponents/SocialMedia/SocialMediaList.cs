using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.Models;

namespace Portfolyo.ViewComponents.SocialMedia
{
    public class SocialMediaList : ViewComponent
    {
        private ISocialMediaService _socialMediaService;

        public SocialMediaList(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _socialMediaService.TGetList(sm => sm.Status == true);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
