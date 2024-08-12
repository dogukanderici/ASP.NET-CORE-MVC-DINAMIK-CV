using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Dashboard
{
    public class FeatureStatistics : ViewComponent
    {
        private ISkillService _skillService;
        private IMessageService _messageService;
        private IExperienceService _experienceService;

        public FeatureStatistics(ISkillService skillService, IMessageService messageService, IExperienceService experienceService)
        {
            _skillService = skillService;
            _messageService = messageService;
            _experienceService = experienceService;
        }
        public IViewComponentResult Invoke()
        {
            var v1Result = _skillService.TGetList();
            var v2Result = _messageService.TGetList(m => m.ReceiverStatus == false && m.SenderStatus == false);
            var v3Result = _messageService.TGetList(m => m.ReceiverStatus == true && m.SenderStatus == false);
            var v4Result = _experienceService.TGetList();

            ViewBag.v1 = v1Result.Data.Count();
            ViewBag.v2 = v2Result.Data.Count();
            ViewBag.v3 = v3Result.Data.Count();
            ViewBag.v4 = v4Result.Data.Count();

            return View();
        }
    }
}
