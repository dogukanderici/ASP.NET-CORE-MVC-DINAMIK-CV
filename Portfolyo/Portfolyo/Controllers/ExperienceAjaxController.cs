using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Portfolyo.Controllers
{
    [Authorize(Policy = "Dynamic-Experience-AjaxRolePolicy")]
    public class ExperienceAjaxController : Controller
    {
        private IExperienceService _experienceService;

        public ExperienceAjaxController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListExperience()
        {
            var result = _experienceService.TGetList();

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var values = JsonConvert.SerializeObject(result.Data);

            return Json(values);
        }

        [HttpPost]
        public IActionResult AddExperience(Experience experience)
        {
            var result = _experienceService.TAdd(experience);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var values = JsonConvert.SerializeObject(experience);

            return Json(values);
        }

        public IActionResult GetById(int id)
        {
            var result = _experienceService.GetById(id);

            if (!result.Success)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            var foundValue = JsonConvert.SerializeObject(result.Data);

            return Json(foundValue);
        }

        public IActionResult DeleteExperience(int id)
        {
            var result = _experienceService.TDelete(id);

            if (!result.Success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            var result = _experienceService.TUpdate(experience);

            if (!result.Success)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
