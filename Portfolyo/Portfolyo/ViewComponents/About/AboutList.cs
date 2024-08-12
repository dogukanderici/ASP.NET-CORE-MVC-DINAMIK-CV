using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.About
{
    public class AboutList : ViewComponent
    {
        private IAboutService _aboutService;

        public AboutList(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public IViewComponentResult Invoke()
        {
            var result = _aboutService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'ı Eklenebilir.
            }

            return View(result.Data);
        }
    }
}
