using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Areas.User.ViewComponents.Navbar
{
    public class NavBar : ViewComponent
    {
        private readonly UserManager<WriterUser> _userManager;

        public NavBar(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.userImageURL = result.ImageUrl;

            return View();
        }
    }
}
