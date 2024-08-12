using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Controllers
{
    public class AdminController : Controller
    {
        public PartialViewResult NewSideBar()
        {
            return PartialView();
        }
        public PartialViewResult NewPartialFooter()
        {
            return PartialView();
        }
        public PartialViewResult NewPartialHead()
        {
            return PartialView();
        }
        public PartialViewResult NewPartialNavBar()
        {
            return PartialView();
        }
        public PartialViewResult NewPartialScripts()
        {
            return PartialView();
        }
    }
}
