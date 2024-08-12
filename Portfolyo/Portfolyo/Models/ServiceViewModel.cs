using Entities.Concrete;

namespace Portfolyo.Models
{
    public class ServiceViewModel
    {
        public Service Service { get; set; }
        public IFormFile ServicePicture { get; set; }
    }
}
