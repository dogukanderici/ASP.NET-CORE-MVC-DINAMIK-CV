using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Skill
{
    public class SkillList : ViewComponent
    {
        private ISkillService _skillService;
        public SkillList(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _skillService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'ı Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
