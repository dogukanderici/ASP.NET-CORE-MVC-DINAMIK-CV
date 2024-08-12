using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Dashboard
{
    public class Last5Projects : ViewComponent
    {
        private IPortfolioService _portfolioService;
        public Last5Projects(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        public IViewComponentResult Invoke()
        {
            var result = _portfolioService.GetLastFiveDataList();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
