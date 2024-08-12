using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Service
{
    public class ServiceList : ViewComponent
    {
        private IServiceService _serviceService;
        public ServiceList(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        public IViewComponentResult Invoke()
        {
            var result = _serviceService.TGetList();

            if (!result.Success)
            {
                return View(); // Hata View'ı Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
