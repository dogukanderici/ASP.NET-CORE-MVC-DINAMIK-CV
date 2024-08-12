using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Dashboard
{
    public class StatisticsDashboard2 : ViewComponent
    {
        private IPortfolioService _portfolioService;
        private IMessageService _messageService;
        private IServiceService _serviceService;
        public StatisticsDashboard2(IPortfolioService portfolioService, IMessageService messageService, IServiceService serviceService)
        {
            _portfolioService = portfolioService;
            _messageService = messageService;
            _serviceService = serviceService;
        }

        public IViewComponentResult Invoke()
        {
            var result1 = _portfolioService.TGetList();
            var result2 = _messageService.TGetList();
            var result3 = _serviceService.TGetList();

            ViewBag.v1 = result1.Data.Count();
            ViewBag.v2 = result2.Data.Count();
            ViewBag.v3 = result3.Data.Count();

            return View();
        }
    }
}
