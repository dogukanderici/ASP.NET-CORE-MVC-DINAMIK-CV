using Entities.Concrete;

namespace Portfolyo.Models
{
    public class ExperienceListViewModel
    {
        public List<Experience> Experiences { get; set; }
        public Experience Experience { get; set; }
        public IFormFile ExperienceImage { get; set; }
    }
}
