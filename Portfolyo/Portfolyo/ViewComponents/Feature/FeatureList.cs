using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Feature
{
    public class FeatureList : ViewComponent
    {
        private IFeatureService _featureService;
        public FeatureList(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public IViewComponentResult Invoke()
        {
            var result = _featureService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata Ekranı Açılabilir.
            }

            return View(result.Data);

        }
    }
}
