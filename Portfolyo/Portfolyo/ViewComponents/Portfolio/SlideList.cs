using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Portfolio
{
    public class SlideList : ViewComponent
    {
        private IPortfolioService _portfolioService;

        public SlideList(IPortfolioService portfolioService)
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
