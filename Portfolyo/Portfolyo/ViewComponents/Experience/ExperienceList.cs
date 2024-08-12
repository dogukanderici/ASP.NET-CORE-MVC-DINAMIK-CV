using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Experience
{
    public class ExperienceList : ViewComponent
    {
        private IExperienceService _experienceService;
        public ExperienceList(IExperienceService experineceService)
        {
            _experienceService = experineceService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _experienceService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
