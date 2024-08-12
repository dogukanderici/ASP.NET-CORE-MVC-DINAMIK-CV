using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core.Metadata.Edm;

namespace Portfolyo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public IActionResult GetExperienceList()
        {
            var result = _experienceService.TGetList();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetExperienceById(int id)
        {
            var result = _experienceService.GetById(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AddExperience(Experience expereince)
        {
            var result = _experienceService.TAdd(expereince);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Message);
        }

        [HttpDelete]
        public IActionResult DeleteExperience(int id)
        {
            var foundData = _experienceService.GetById(id);

            if (foundData.Data == null)
            {
                return NotFound();
            }

            var result = _experienceService.TDelete(id);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Message);
        }

        [HttpPut]
        public IActionResult UpdateExperience(Experience experience)
        {
            var foundData = _experienceService.GetById(experience.ExperienceId);

            if (foundData.Data == null)
            {
                return NotFound();
            }

            var result = _experienceService.TUpdate(experience);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Message);
        }
    }
}
