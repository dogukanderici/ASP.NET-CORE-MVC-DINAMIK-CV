using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portfolyo.Models;

namespace Portfolyo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelRoleController : Controller
    {
        private IPanelRoleService _panelService;
        private IWriterRoleService _writerRoleService;

        public PanelRoleController(IPanelRoleService panelService, IWriterRoleService writerRoleService)
        {
            _panelService = panelService;
            _writerRoleService = writerRoleService;
        }

        public IActionResult Index(string panelName = null, string queryType = null)
        {
            var result = _panelService.TGetList(panelName != null ? p => p.PanelName == panelName : null);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return queryType == null ? View(result.Data) : Json(JsonConvert.SerializeObject(result.Data));
        }

        [HttpGet]
        public IActionResult AddPanelRole()
        {
            var result = _writerRoleService.TGetList();

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var model = new PanelRoleListViewModel
            {
                WriterRoles = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddPanelRole(PanelRoleListViewModel panelRoleListViewModel)
        {
            var result = _panelService.TAdd(panelRoleListViewModel.PanelRole);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return RedirectToAction("Index", new { panelName = panelRoleListViewModel.PanelRole.PanelName });
        }

        [HttpGet]
        public IActionResult UpdatePanelRole(int id)
        {
            var result = _panelService.GetById(id);
            var roleResult = _writerRoleService.TGetList();

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var model = new PanelRoleListViewModel
            {
                WriterRoles = roleResult.Data,
                PanelRole = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdatePanelRole(PanelRole panelRole)
        {
            var result = _panelService.TUpdate(panelRole);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return RedirectToAction("Index", new { panelName = panelRole.PanelName });
        }

        public IActionResult DeletePanelRole(int id)
        {
            var result = _panelService.TDelete(id);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return RedirectToAction("Index");
        }
    }
}
