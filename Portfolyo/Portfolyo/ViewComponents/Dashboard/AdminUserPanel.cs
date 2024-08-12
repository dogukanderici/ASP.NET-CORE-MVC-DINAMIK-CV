using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.Dashboard
{
    public class AdminUserPanel : ViewComponent
    {
        private readonly UserManager<WriterUser> _userManager;

        public AdminUserPanel(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.AdminName = result.Name + ' ' + result.Surname;
            ViewBag.ImageUrl = result.ImageUrl;

            return View();
        }
    }
}
