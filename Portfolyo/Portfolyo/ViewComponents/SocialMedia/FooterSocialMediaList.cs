using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.SocialMedia
{
    public class FooterSocialMediaList : ViewComponent
    {
        private ISocialMediaService _socialMediaService;

        public FooterSocialMediaList(ISocialMediaService socialMediaService)
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
